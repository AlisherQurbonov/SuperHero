using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace superhero;

public class SuperHero
{

    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
 
    [Column(TypeName = "nvarchar(50)")] 

    public string Name { get; set; } 


    [Column(TypeName = "nvarchar(30)")] 

    public string FirstName { get; set; } 

   
    [Column(TypeName = "nvarchar(30)")] 

    public string LastName { get; set; } 

   
    [Column(TypeName = "nvarchar(50)")] 

    public string Place { get; set; } 




    public SuperHero(int id, string name, string firstName, string lastName, string place)
    {
        Id = id;
        Name = name;
        FirstName = firstName;
        LastName = lastName;
        Place = place;
    }
    
}