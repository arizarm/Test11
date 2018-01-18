using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Message
/// </summary>
public class Message
{
    public const String EmptySupplierCode
    = "Supplier Code cannot be empty.  Please enter a Supplier Code.";

    public const String EmptySupplierName
    = "Supplier Name cannot be empty.  Please enter a Supplier Name.";

    public const String EmptyContactName
    = "Contact Name cannot be empty.  Please enter a Contact Name.";

    public const String PhoneNo
    = "Phone No cannot be empty.  Please enter a Phone Number.";

    public const String FaxNo
    = "Fax No cannot be empty.  Please enter a Fax Number.";

    public const String Address
        = "Invalid Address.  Please enter a valid address.";

    public const String AddSupplierSuccessful
        = "Supplier record successfully created!";

    public const String OneItemPerSupplier
        = "Maximum 1 item per supplier in a year!";

    public const String InvalidEntry
        = "Please input valid Category & Item!";

    public const String PageInvalidEntry
        = "Please go through the proper page!";

    public const String SuccessfulItemAdd
        = "Item Added Successfully!";

    public const String UpdateSuccessful
        = "Update Successful!";

    public const String InactiveSuccessful
        = "Supplier successfuly set to Inactive!";

    public const String SupplierSuccessfulAdd
        = "Supplier Added Successfully!";

    public const String DeleteSuccessful
        = "Deleted Successfully!";

    public const String CategoryAlreadyInList
        = "Category is already in list!";

    public const String DepartmentAlreadyInList
        = "Department is already in list!";
}