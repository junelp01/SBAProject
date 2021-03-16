namespace Edi.View
{
  using System.IO;
  using System.Windows;
  using System.Windows.Input;
  using AvalonDock.Layout.Serialization;
  using Edi.Command;
  using Edi.ViewModel;

    using System.Windows.Media;

  using System.Windows.Controls;
  using unvell.ReoGrid;

  using unvell.ReoGrid.CellTypes;
  using unvell.ReoGrid.Chart;
  using unvell.ReoGrid.Drawing.Shapes;


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = Workspace.This;

            Workspace.This.InitCommandBinding(this);

            //Loadgrid();
        }


        #region LoadLayoutCommand
        RelayCommand _loadLayoutCommand = null;
        public ICommand LoadLayoutCommand
        {
            get
            {
                if (_loadLayoutCommand == null)
                {
                    _loadLayoutCommand = new RelayCommand((p) => OnLoadLayout(p), (p) => CanLoadLayout(p));
                }

                return _loadLayoutCommand;
            }
        }

        private bool CanLoadLayout(object parameter)
        {
            return File.Exists(@".\AvalonDock.Layout.config");
        }

        private void OnLoadLayout(object parameter)
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            //Here I've implemented the LayoutSerializationCallback just to show
            // a way to feed layout desarialization with content loaded at runtime
            //Actually I could in this case let AvalonDock to attach the contents
            //from current layout using the content ids
            //LayoutSerializationCallback should anyway be handled to attach contents
            //not currently loaded
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
                {
                    //if (e.Model.ContentId == FileStatsViewModel.ToolContentId)
                    //    e.Content = Workspace.This.FileStats;
                    //else if (!string.IsNullOrWhiteSpace(e.Model.ContentId) &&
                    //    File.Exists(e.Model.ContentId))
                    //    e.Content = Workspace.This.Open(e.Model.ContentId);
                };
            layoutSerializer.Deserialize(@".\AvalonDock.Layout.config");
        }

        #endregion 

        #region SaveLayoutCommand
        RelayCommand _saveLayoutCommand = null;
      

        public ICommand SaveLayoutCommand
        {
            get
            {
                if (_saveLayoutCommand == null)
                {
                    _saveLayoutCommand = new RelayCommand((p) => OnSaveLayout(p), (p) => CanSaveLayout(p));
                }

                return _saveLayoutCommand;
            }
        }

        private bool CanSaveLayout(object parameter)
        {
            return true;
        }

        private void OnSaveLayout(object parameter)
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.Serialize(@".\AvalonDock.Layout.config");
        }

        #endregion 

        private void OnDumpToConsole(object sender, RoutedEventArgs e)
        {
#if DEBUG
            dockManager.Layout.ConsoleDump(0);
#endif
        }









        //void Loadgrid()
        //{

        //    // don't use Clear method in actual application,
        //    // instead, load template into the first worksheet directly.
        //    grid.Worksheets.Clear();

        //    // handles event to update menu check status.
        //    grid.SettingsChanged += (s, e) => UpdateMenuChecks();
        //    grid.CurrentWorksheetChanged += (s, e) => UpdateMenuChecks();

        //    // add demo sheet 1: document template
        //    //AddDemoSheet1();
        //}


        //private void UpdateMenuChecks()
        //{
        //    var sheet = grid.CurrentWorksheet;
        //}
        //#region Demo Sheet 1 : Document Template
        //private void AddDemoSheet1()
        //{
        //    /****************** Sheet1 : Document Template ********************/
        //    var worksheet = grid.NewWorksheet("Document");

        //    // load template
        //    //using (MemoryStream ms = new MemoryStream(Properties.Resources.order_sample))
        //    //{
        //    //	worksheet.LoadRGF(ms);
        //    //}

        //    // fill data into worksheet
        //    var dataRange = worksheet.Ranges["A2:G35"];

        //    dataRange.Data = new object[,]
        //    {   {"Product ID", "Description", "Qty", "Price", "Total Amount"},
        //        {"[23423423]", "Product ABC", 15, 150, 2250},
        //        {"[45645645]", "Product DEF", 1, 75, 75},
        //        {"[78978978]", "Product GHI", 2, 30, 60},
        //    };

        //}
        //#endregion // Demo Sheet 1 : Document Template
    }
}
