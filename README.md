To run this order service API we need to setup few things in appsetting file. 
  1. We need to provide database connection string to connect local database.
  2. Customer details like name and email is hardcoded. If you want to call this API methods please add your name and email. As currently token based authentication is implemented using JwT.

Functionality of Order Service API
  1. Place Order
  2. GetOrderById
  3. GetOrderByCustomerName
  4. GetOrders

Also logging functionality used to log message in console.
