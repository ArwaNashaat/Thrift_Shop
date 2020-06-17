using p2.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2
{
    class Program
    {
        static void showProduct() {

            Thrift_ShopDataContext dbContext = new Thrift_ShopDataContext();
            IEnumerable<Product> product = from Product in dbContext.Products
                                           join Brand in dbContext.Brands
                                           on Product.brandID equals Brand.id into ID
                                           select Product;


            Console.WriteLine("Product Name     " + "Product Price     " + "Category     " + "Brand Name");
            foreach (Product p in product)
                Console.WriteLine(p.name + "        " + p.price + "         " + p.category + "        " + p.Brand.brandName);

        }

        static void addProduct()
        {
            String name, category, brandName;
            int price;
            Thrift_ShopDataContext dbContext = new Thrift_ShopDataContext();
            int id = (from product in dbContext.Products
                      select product).Max(product => product.id);


            Console.WriteLine("Enter Product Name Product Price Category Brand Name");

            name = Console.ReadLine();
            price = int.Parse(Console.ReadLine());
            category = Console.ReadLine();
            brandName = Console.ReadLine();

            int brandId = (from brand in dbContext.Brands
                           where brand.brandName == brandName
                           select brand.id).FirstOrDefault();
            while (brandId == 0)
            {
                Console.WriteLine("No such brand name Enter another one");

                brandName = Console.ReadLine();

                brandId = (from brand in dbContext.Brands
                           where brand.brandName == brandName
                           select brand.id).FirstOrDefault();

            }

            Product prod = new Product
            {
                id = id + 1,
                name = name,
                price = price,
                category = category,
                brandID = brandId
            };


            dbContext.Products.InsertOnSubmit(prod);
            dbContext.SubmitChanges();
        }

        static void filterProduct()
        {
            double price;
            Thrift_ShopDataContext dbContext = new Thrift_ShopDataContext();
            Console.WriteLine("Enter price:");

            price = double.Parse(Console.ReadLine());

            var p = from product in dbContext.Products
                    where product.price <= price
                    select product;

            Console.WriteLine("Product Name" + "        " + "Product price");
            foreach (var i in p)
                Console.WriteLine(i.name + "        " + i.price);

        }

        static void showBrand() {
            Thrift_ShopDataContext dbContext = new Thrift_ShopDataContext();

            var product = from Brand in dbContext.Brands
                          join Product in dbContext.Products
                          on Brand.id equals Product.brandID into ID
                          select new {
                              name = Brand.brandName,
                              count = ID.Count(),

                          };

            Console.WriteLine("Product Name" + "        " + "Numbers of products");
            foreach (var i in product)
                Console.WriteLine(i.name + "        " + i.count);

        }

        static void nameAscending() {
            Thrift_ShopDataContext dbContext = new Thrift_ShopDataContext();

            var product = from Product in dbContext.Products
                          orderby Product.name ascending
                          select Product;

            Console.WriteLine("Product Name     " + "Product Price     " + "Category     " + "Brand Name");
            foreach (Product p in product)
                Console.WriteLine(p.name + "        " + p.price + "         " + p.category + "        " + p.Brand.brandName);

        }

        static void nameDescending()
        {
            Thrift_ShopDataContext dbContext = new Thrift_ShopDataContext();

            var product = from Product in dbContext.Products
                          orderby Product.name descending
                          select Product;

            Console.WriteLine("Product Name     " + "Product Price     " + "Category     " + "Brand Name");
            foreach (Product p in product)
                Console.WriteLine(p.name + "        " + p.price + "         " + p.category + "        " + p.Brand.brandName);

        }

        static void priceAscending()
        {
            Thrift_ShopDataContext dbContext = new Thrift_ShopDataContext();

            var product = from Product in dbContext.Products
                          orderby Product.price ascending
                          select Product;

            Console.WriteLine("Product Name     " + "Product Price     " + "Category     " + "Brand Name");
            foreach (Product p in product)
                Console.WriteLine(p.name + "        " + p.price + "         " + p.category + "        " + p.Brand.brandName);

        }

        static void priceDescending()
        {
            Thrift_ShopDataContext dbContext = new Thrift_ShopDataContext();

            var product = from Product in dbContext.Products
                          orderby Product.price descending
                          select Product;

            Console.WriteLine("Product Name     " + "Product Price     " + "Category     " + "Brand Name");
            foreach (Product p in product)
                Console.WriteLine(p.name + "        " + p.price + "         " + p.category + "        " + p.Brand.brandName);

        }

        static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine("0) Exit" +
                    "\n1) Show Product \n2)Add Product \n3)Filter product by price" +
                    "\n4) Show brands \n5) Sort products");
                int key;
                key = int.Parse(Console.ReadLine());

                switch (key) {
                    case 0:
                        return;
                    case 1:
                        showProduct();
                        break;
                    case 2:
                        addProduct();
                        break;
                    case 3:
                        filterProduct();
                        break;

                    case 4:
                        showBrand();
                        break;

                    case 5:
                        Console.WriteLine("1) Sort by name \n2) Sort by price");
                        int sort = int.Parse(Console.ReadLine());

                        switch (sort) {
                            case 1:
                                Console.WriteLine("1) Sort ascending \n2) Sort descending");
                                int direction = int.Parse(Console.ReadLine());

                                switch (direction)
                                {
                                    case 1:
                                        nameAscending();
                                        break;

                                    case 2:
                                        nameDescending();
                                        break;

                                }
                                break;

                            case 2:
                                Console.WriteLine("1) Sort ascending \n2) Sort descending");
                                direction = int.Parse(Console.ReadLine());

                                switch (direction)
                                {
                                    case 1:
                                        priceAscending();
                                        break;

                                    case 2:
                                        priceDescending();
                                        break;

                                }
                                break;
                        }
                    
                        break;

                }

            }            
        }
    }
}
