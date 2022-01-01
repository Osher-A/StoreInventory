# StoreManagement Overview
>StoreMangaement App is a desktop application that can be used to manage a store, controlling the store's inventory, creating customer orders, and keeping track of the status of all orders. This App hasn't (yet) been tested in real, it was written as a way to advance my development skills.

## Core Features
* Add Products to inventory
* Keep track of inventory status
* Submitting a order
* Keep track of all orders
* Email a reminder for unpaid bills

## Built With
##### Back end
* C# 
* MSSql
* Entity Framework Core
##### Front end
* WPF/xaml 
* MahApps


## Features in more detail
There is currently three pages to the project (besides the Home page). The pages are responsive and have the option to navigate backwords and forwards.  
To see this in action [check this out](https://user-images.githubusercontent.com/70821594/147895136-55f73153-1027-4804-9f8c-17b275646033.mp4 "Navigation demo")

#### Page 1 - [Inventory page](https://user-images.githubusercontent.com/70821594/147864691-6c522b90-dc68-4381-a9db-1414ee25e1cf.mp4 "Inventory page") - Features
* Add new product details, including a picture which can be uploaded (via the provided link) from the users computer
* Update/delete a product thats selected from the DataGrid
* The status for low in stock products are highlighted in red font (in the data grid), and for out of stock products it would blink continuously 
* Search a product by product name
* Seperate DataGrid for Low in stock products and for out of stock products
#### Page 2 - Order page - Features
* There is an option to insert a customers details (needed if not paying for order)
* Search a product by category name and/or by product name
* Once a product is selected from the grid, it gets added to the Shopping Basket grid
* Each time the same product is selected again, the quantity for that product gets updated in the Shopping Basket grid
* In the Shopping Basket grid, there is a running total for each product
* If the Not-paid/Partly-paid Radio button is selected and the customers details hasn't been inserted then a message box appears to fill in required details
#### Page 3 - Order Controller page - Features
* Three Data Grids, one for all orders, one for search orders, and one for unpaid orders
* Search Grid to search orders by date, name, address, or email
* Update payment by first selecting a order from one of the grids
* Send an email reminder if order not paid, just with one click, the email gets generated with all the order details








