#Mastering Windows Presentation Foundation
This is the code repository for [Mastering Windows Presentation Foundation](https://www.packtpub.com/application-development/mastering-windows-presentation-foundation?utm_source=github&utm_medium=repository&utm_campaign=9781785883002), published by [Packt](https://www.packtpub.com/?utm_source=github). It contains all the supporting project files necessary to work through the book from start to finish.
## About the Book
Windows Presentation Foundation is rich in possibilities when it comes to delivering an excellent user experience. This book will show you how to build professional-grade applications that look great and work smoothly.

##Instructions and Navigation
All of the code is organized into folders. Each folder starts with a number followed by the application name. For example, Chapter02.

This code bundle represents all of the main coding classes that were introduced in the Mastering Windows Presentation Foundation book.

The folder structure follows the two folder structure diagrams in chapter 1, A Smarter Way of Working with WPF.

The main View can be changed in the CompanyName.ApplicationName.ViewModels.MainWindowViewModel.cs class and is currently set to the TextViewModel class, which displays the TextView class:

    public MainWindowViewModel() : base()
    {
        ViewModel = new TextViewModel(); <---
    }

Please note that this is NOT a demonstration application, but instead it represents a basic application framework that you can build your future WPF applications upon, with added examples from the book.

Also note that there are some classes here that represent different stages of development, or examples from the book. For example, you will find various Product classes and each will have matching ProductView and ProductViewModel classes that demonstrate the various ways to validate, as shown in the book.

Likewise, you'll also find two versions of the CompanyName.ApplicationName.Views.Panels.AnimatedStackPanel.cs class that follow the examples in the book.

Please build the Solution before attempting to run it.

The code will look like the following:
```
public string Name
{
get { return name; }
set
{
  if (name != value)
  {
    name = value;
    NotifyPropertyChanged("Name");
    }
  }
}
```

As with all WPF development, you'll need to have the .NET Framework and a version of
Microsoft's Visual Studio integrated development environment software installed on your
computer.
You'll be able to use versions as old as 2010, but in order to use the code in the book that
takes advantage of the latest .NET Framework improvements, you'll need to use one of the
newer versions. Note that any edition of Visual Studio can be used, from the top of the line
Enterprise edition to the free Community (2015) edition.

##Related Products
* [Mastering Windows Server 2016](https://www.packtpub.com/networking-and-servers/mastering-windows-server-2016?utm_source=github&utm_medium=repository&utm_campaign=9781785888908)

* [Mastering Windows PowerShell 5 Administration [Video]](https://www.packtpub.com/networking-and-servers/mastering-windows-powershell-5-administration-video?utm_source=github&utm_medium=repository&utm_campaign=9781786467980)

* [Microsoft Dynamics NAV 2013 Application Design](https://www.packtpub.com/application-development/microsoft-dynamics-nav-2013-application-design?utm_source=github&utm_medium=repository&utm_campaign=9781782170365)

###Suggestions and Feedback
[Click here](https://docs.google.com/forms/d/e/1FAIpQLSe5qwunkGf6PUvzPirPDtuy1Du5Rlzew23UBp2S-P3wB-GcwQ/viewform) if you have any feedback or suggestions.
