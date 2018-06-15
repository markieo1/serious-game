using Accord.Controls;
using Accord.MachineLearning;
using Accord.Math;
using SeriousGameClustering.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace SeriousGameClustering
{
	public partial class MainForm : Form
	{
		ColorSequenceCollection colors = new ColorSequenceCollection();

		public MainForm()
		{
			InitializeComponent();
		}

		private void btnOpenFile_Click(object sender, EventArgs e)
		{
			var fileDialog = new System.Windows.Forms.OpenFileDialog();
			if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string fileToOpen = fileDialog.FileName;

				StartClustering(fileToOpen);
			}
		}

		private void StartClustering(string filePath)
		{
			var readEvents = FileHelper.ReadAndConvert(filePath);
			var clusteringModels = ClusteringHelper.ConvertToClusteringModel(readEvents);

			// Next we need the datatable
			DataTable clusteringTable = DataTableHelper.ConvertListToDataTable(clusteringModels);

			double[][] jaggedClusteringObservations = clusteringTable.ToJagged();

			CreateScatterplot(graph, jaggedClusteringObservations, 3);


			// Create a new K-Means algorithm with 3 clusters 
			KMeans kmeans = new KMeans(3);

			// Compute the algorithm, retrieving an integer array
			//  containing the labels for each of the observations
			KMeansClusterCollection clusters = kmeans.Learn(jaggedClusteringObservations);

			// As a result, the first two observations should belong to the
			//  same cluster (thus having the same label). The same should
			//  happen to the next four observations and to the last three.
			int[] labels = clusters.Decide(jaggedClusteringObservations);
			UpdateGraph(labels, jaggedClusteringObservations);
		}

		private void UpdateGraph(int[] classifications, double[][] observations)
		{
			// Paint the clusters accordingly
			for (int i = 0; i < 3 + 1; i++)
				graph.GraphPane.CurveList[i].Clear();

			for (int j = 0; j < observations.Length; j++)
			{
				int c = classifications[j];

				var curveList = graph.GraphPane.CurveList[c + 1];
				double[] point = observations[j];
				curveList.AddPoint(point[0], point[1]);
			}

			graph.Invalidate();
		}

		public void CreateScatterplot(ZedGraphControl zgc, double[][] graph, int n)
		{
			GraphPane myPane = zgc.GraphPane;
			myPane.CurveList.Clear();

			// Set graph pane object
			myPane.Title.Text = "Normal (Gaussian) Distributions";
			myPane.XAxis.Title.Text = "X";
			myPane.YAxis.Title.Text = "Y";
			myPane.XAxis.Scale.Max = 10;
			myPane.XAxis.Scale.Min = -10;
			myPane.YAxis.Scale.Max = 10;
			myPane.YAxis.Scale.Min = -10;
			myPane.XAxis.IsAxisSegmentVisible = false;
			myPane.YAxis.IsAxisSegmentVisible = false;
			myPane.YAxis.IsVisible = false;
			myPane.XAxis.IsVisible = false;
			myPane.Border.IsVisible = false;


			// Create mixture pairs
			PointPairList list = new PointPairList();
			for (int i = 0; i < graph.Length; i++)
				list.Add(graph[i][0], graph[i][1]);


			// Add the curve for the mixture points
			LineItem myCurve = myPane.AddCurve("Mixture", list, Color.Gray, SymbolType.Diamond);
			myCurve.Line.IsVisible = false;
			myCurve.Symbol.Border.IsVisible = false;
			myCurve.Symbol.Fill = new Fill(Color.Gray);

			for (int i = 0; i < n; i++)
			{
				// Add curves for the clusters to be detected
				Color color = colors[i];
				myCurve = myPane.AddCurve("D" + (i + 1), new PointPairList(), color, SymbolType.Diamond);
				myCurve.Line.IsVisible = false;
				myCurve.Symbol.Border.IsVisible = false;
				myCurve.Symbol.Fill = new Fill(color);
			}

			// Fill the background of the chart rect and pane
			myPane.Fill = new Fill(Color.WhiteSmoke);

			zgc.AxisChange();
			zgc.Invalidate();
		}
	}
}
