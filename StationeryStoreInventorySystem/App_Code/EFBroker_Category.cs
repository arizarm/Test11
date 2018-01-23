using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Category
/// </summary>
public class EFBroker_Category
{
    public EFBroker_Category()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public List<Category> GetCategoryList()
    {
        List<Category> categories;
        using(StationeryEntities categoryDB = new StationeryEntities()) { 
        categories = categoryDB.Categories.OrderBy(x => x.CategoryID).ToList();
        }
        return categories;
    }
    public List<string> GetAllCategoryNames()
    {
        List<string> categoryNames;
        using (StationeryEntities categoryDB = new StationeryEntities())
        {
            categoryNames = categoryDB.Categories.Select(x => x.CategoryName).ToList();
        }
        return categoryNames;
    }
    public Category GetCategorybyID(int categoryID)
    {
        Category cat;
        using (StationeryEntities categoryDB = new StationeryEntities())
        {
            cat = categoryDB.Categories.Where(x => x.CategoryID == categoryID).FirstOrDefault();
        }
        return cat;
    }
    public Category GetCategorybyName(string categoryName)
    {
        Category cat;
        using (StationeryEntities categoryDB = new StationeryEntities())
        {
            cat = categoryDB.Categories.Where(x => x.CategoryName == categoryName).FirstOrDefault();
        }
        return cat;
    }
    public void AddCategory(Category category)
    {
        using (StationeryEntities categoryDB = new StationeryEntities())
        {
            categoryDB.Categories.Add(category);
            categoryDB.SaveChanges();
        }
        return;
    }
}