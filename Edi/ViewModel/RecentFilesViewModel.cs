namespace Edi.ViewModel
{
  using System;
  using System.IO;

  using SimpleControls.MRU.Model;
  using SimpleControls.MRU.ViewModel;


    using FontAwesome.WPF;
    using System.Windows.Media;

    internal class RecentFilesViewModel : Base.ToolViewModel
  {
    private MRUListVM mMruList;

    public const string ToolContentId = "RecentFilesTool";

    public RecentFilesViewModel()
      : base("Tool VIew - Left ")
    {
      ////Workspace.This.ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);
      ContentId = ToolContentId;

      this.mMruList = new MRUListVM();
      
     //part of issue no. 60(icon) -edited by reeden
      IconSource = ImageAwesome.CreateImageSource(FontAwesomeIcon.Home, Brushes.Black, 15);//bi;
        }

    //public override Uri IconSource
    //{
    //  get
    //  {
    //    return new Uri("pack://application:,,,/SimpleControls;component/MRU/Images/NoPin16.png", UriKind.RelativeOrAbsolute);
    //  }
    //}

    public MRUListVM MruList
    {
      get
      {
        return this.mMruList;
      }

      private set
      {
        if (this.mMruList != value)
        {
          this.mMruList = value;
          this.NotifyPropertyChanged(() => this.MruList);
        }
      }
    }

    public void AddNewEntryIntoMRU(string filePath)
    {
      if (this.MruList.FindMRUEntry(filePath) == null)
      {
        MRUEntryVM e = new MRUEntryVM() { IsPinned = false, PathFileName = filePath };

        this.MruList.AddMRUEntry(e);

        this.NotifyPropertyChanged(() => this.MruList);
      }
    }
  }
}
