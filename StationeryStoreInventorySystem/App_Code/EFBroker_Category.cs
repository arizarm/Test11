using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Category
/// </summary>
public class EFBroker_Category
{
    StationeryEntities categoryDB;
    public EFBroker_Category()
    {
        categoryDB = new StationeryEntities();
        //
        // TODO: Add constructor logic here
        //
    }
    public List<Category> GetCategoryList()
    {
        List<Category> categories = categoryDB.Categories.OrderBy(x => x.CategoryID).ToList();
        return categories;
    }
    public Category GetCategorybyID(int categoryID)
    {
        Category cat = categoryDB.Categories.Where(x => x.CategoryID == categoryID).FirstOrDefault();
        return cat;
    }
    public Category GetCategorybyName(string categoryName)
    {
        Category cat = categoryDB.Categories.Where(x => x.CategoryName == categoryName).FirstOrDefault();
        return cat;
    }
    public void AddCategory(Category category)
    {
        categoryDB.Categories.Add(category);
        categoryDB.SaveChanges();
    }
}