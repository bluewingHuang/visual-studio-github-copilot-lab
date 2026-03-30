using DataEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TinyShop.Tests;

[TestClass]
public class ProductTests
{
    [TestMethod]
    // Verify a newly constructed Product has expected default values
    public void Product_NewInstance_HasDefaultValues()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.AreEqual(0, product.Id, "Default Id should be 0");
        Assert.IsNull(product.Name, "Default Name should be null");
        Assert.IsNull(product.Description, "Default Description should be null");
        Assert.AreEqual(0m, product.Price, "Default Price should be 0");
        Assert.IsNull(product.ImageUrl, "Default ImageUrl should be null");
    }

    [TestMethod]
    // Ensure Id property can be set and retrieved correctly, including large values
    public void Product_Id_SetGet_Succeeds()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = int.MaxValue;

        // Assert
        Assert.AreEqual(int.MaxValue, product.Id, "Id should round-trip the assigned value");
    }

    [TestMethod]
    // Ensure all properties can be set and read back correctly
    public void Product_AllProperties_SetGet_Values()
    {
        // Arrange
        var product = new Product();
        var id = 42;
        var name = "Camping Tent";
        var desc = "A durable 4-person tent for all seasons.";
        var price = 199.99m;
        var image = "images/tent.png";

        // Act
        product.Id = id;
        product.Name = name;
        product.Description = desc;
        product.Price = price;
        product.ImageUrl = image;

        // Assert
        Assert.AreEqual(id, product.Id, "Id should match the value set");
        Assert.AreEqual(name, product.Name, "Name should match the value set");
        Assert.AreEqual(desc, product.Description, "Description should match the value set");
        Assert.AreEqual(price, product.Price, "Price should match the value set");
        Assert.AreEqual(image, product.ImageUrl, "ImageUrl should match the value set");
    }

    [DataTestMethod]
    [DataRow("Tent")]
    [DataRow("")]
    [DataRow(" ")] // whitespace
    [DataRow(null)]
    // Verify Name accepts common and edge-case string values
    public void Product_Name_DataRow_SetGet(string name)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = name;

        // Assert
        Assert.AreEqual(name, product.Name, "Name should round-trip the assigned value");
    }

    [DataTestMethod]
    [DataRow("Short description")]
    [DataRow("")]
    [DataRow(null)]
    // Verify Description accepts typical and edge-case values
    public void Product_Description_DataRow_SetGet(string description)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Description = description;

        // Assert
        Assert.AreEqual(description, product.Description, "Description should round-trip the assigned value");
    }

    [DataTestMethod]
    [DataRow(0.0)]
    [DataRow(19.99)]
    [DataRow(-5.5)]
    [DataRow(10000000.123)]
    // Verify Price property preserves numeric values and precision when set
    public void Product_Price_DataRow_SetGet(double input)
    {
        // Arrange
        var product = new Product();
        var expected = (decimal)input;

        // Act
        product.Price = expected;

        // Assert
        Assert.AreEqual(expected, product.Price, $"Price should round-trip the assigned value: {expected}");
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("img.png")]
    [DataRow("/assets/images/product.jpg")]
    // Verify ImageUrl accepts null, empty and typical URL/path values
    public void Product_ImageUrl_DataRow_SetGet(string imageUrl)
    {
        // Arrange
        var product = new Product();

        // Act
        product.ImageUrl = imageUrl;

        // Assert
        Assert.AreEqual(imageUrl, product.ImageUrl, "ImageUrl should round-trip the assigned value");
    }
}
