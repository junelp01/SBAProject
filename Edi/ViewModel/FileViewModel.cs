﻿namespace Edi.ViewModel
{
  using System;
  using System.IO;
  using System.Windows.Input;
  using System.Windows.Media;
  using Edi.Command;

  using FontAwesome.WPF;

  class FileViewModel : Base.FileBaseViewModel
  {
    public FileViewModel(string filePath)
    {
      FilePath = filePath;
      Title = FileName;

         IconSource = ImageAwesome.CreateImageSource(FontAwesomeIcon.Dashboard, Brushes.Black, 15);//bi;
        }

    public FileViewModel()
    {
      IsDirty = true;
      Title = FileName;

      IconSource = ImageAwesome.CreateImageSource(FontAwesomeIcon.Dashboard, Brushes.Black, 15);//bi;

        }

    #region FilePath
    private string _filePath = null;
    override public string FilePath
    {
      get { return _filePath; }
      protected set
      {
        if (_filePath != value)
        {
          _filePath = value;
          RaisePropertyChanged("FilePath");
          RaisePropertyChanged("FileName");
          RaisePropertyChanged("Title");

          if (File.Exists(_filePath))
          {
            _textContent = File.ReadAllText(_filePath);
            ContentId = _filePath;
          }
        }
      }
    }
    #endregion

    public string FileName
    {
      get
      {
        if (FilePath == null)
          return "Document View" + (IsDirty ? "*" : "") ;

        return System.IO.Path.GetFileName(FilePath) + (IsDirty ? "*" : "");
      }
    }

    #region TextContent

    private string _textContent = string.Empty;
    public string TextContent
    {
      get { return _textContent; }
      set
      {
        if (_textContent != value)
        {
          _textContent = value;
          RaisePropertyChanged("TextContent");
          IsDirty = true;
        }
      }
    }

    #endregion

    #region IsDirty

    private bool _isDirty = false;
    override public bool IsDirty
    {
      get { return _isDirty; }
      set
      {
        if (_isDirty != value)
        {
          _isDirty = value;
          RaisePropertyChanged("IsDirty");
          RaisePropertyChanged("FileName");
        }
      }
    }

    #endregion

    #region SaveCommand
    RelayCommand _saveCommand = null;
    override public ICommand SaveCommand
    {
      get
      {
        if (_saveCommand == null)
        {
          _saveCommand = new RelayCommand((p) => OnSave(p), (p) => CanSave(p));
        }

        return _saveCommand;
      }
    }

    public bool CanSave(object parameter)
    {
      return IsDirty;
    }

    private void OnSave(object parameter)
    {
      Workspace.This.Save(this, false);
    }

    #endregion

    #region SaveAsCommand
    RelayCommand _saveAsCommand = null;
    public ICommand SaveAsCommand
    {
      get
      {
        if (_saveAsCommand == null)
        {
          _saveAsCommand = new RelayCommand((p) => OnSaveAs(p), (p) => CanSaveAs(p));
        }

        return _saveAsCommand;
      }
    }

    private bool CanSaveAs(object parameter)
    {
      return IsDirty;
    }

    private void OnSaveAs(object parameter)
    {
      Workspace.This.Save(this, true);
    }

    #endregion

    #region CloseCommand
    RelayCommand _closeCommand = null;
    override public ICommand CloseCommand
    {
      get
      {
        if (_closeCommand == null)
        {
          _closeCommand = new RelayCommand((p) => OnClose(), (p) => CanClose());
        }

        return _closeCommand;
      }
    }

    private bool CanClose()
    {
      return true;
    }

    private void OnClose()
    {
      Workspace.This.Close(this);
    }
    #endregion

    //public override Uri IconSource
    //{
    //  get
    //  {
    //    // This icon is visible in AvalonDock's Document Navigator window
    //    return new Uri("pack://application:,,,/Edi;component/Images/document.png", UriKind.RelativeOrAbsolute);
    //  }
    //}

    public void SetFileName(string f)
    {
      this._filePath = f;
    }
  }
}
