using People.Models;
using SQLite;

namespace People;

public class PersonRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }
    private SQLiteConnection _conn{ get; set; }

    // TODO: Add variable for the SQLite connection

    private void Init()
    {
 
    if(_conn != null)
      return;
        _conn= new SQLiteConnection(_dbPath);
        _conn.CreateTable<JSPerson>();  
        
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public void AddNewPerson(string name)
    {            
        int result = 0;
        try
        {
            // TODO: Call Init()
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Debes ingresar un nombre para ingresar a la persona");

            // TODO: Insert the new person into the database
            result = _conn.Insert(new JSPerson { Name = name });
           // result = 0;

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }

    public List<JSPerson> GetAllPeople()
    {

        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            Init();
            return _conn.Table<JSPerson>().ToList();
            
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<JSPerson>();
    }
}
