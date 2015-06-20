using LiteDB;
using PodCatchup.Infrastructure;
using PodCatchup.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodCatchup.Services
{
  public class LiteDBLibrarySaver : ILibrarySaver
  {
    public void SaveLibrary(ref PodcastLibrary library)
    {
      String mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      String appdir = Path.Combine(mydocs, "PodCatchup");
      if(!Directory.Exists(appdir))
      {
        Directory.CreateDirectory(appdir);
      }
      String savefilename = Path.Combine(appdir, "PodCatchup.db");
      using (var db = new LiteDatabase(savefilename))
      {
        var col = db.GetCollection<PodcastLibrary>("customers");
        if (library.Id == 0)
        {
          col.Insert(library);
        }
        else
        {
          col.Update(library);
        }
      }
    }
  }
}
