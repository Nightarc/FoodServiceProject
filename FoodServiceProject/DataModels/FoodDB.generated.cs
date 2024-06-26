//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Collections.Generic;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : postgres
	/// Data Source    : tcp://localhost:5432
	/// Server Version : 16.2
	/// </summary>
	public partial class PostgresDB : LinqToDB.Data.DataConnection
	{
		public ITable<Customer>         Customers         { get { return this.GetTable<Customer>(); } }
		public ITable<DeliveryPoint>    DeliveryPoints    { get { return this.GetTable<DeliveryPoint>(); } }
		public ITable<Food>             Foods             { get { return this.GetTable<Food>(); } }
		public ITable<Order>            Orders            { get { return this.GetTable<Order>(); } }
		public ITable<OrderDetail>      OrderDetails      { get { return this.GetTable<OrderDetail>(); } }
		public ITable<Payment>          Payments          { get { return this.GetTable<Payment>(); } }
		public ITable<Promotion>        Promotions        { get { return this.GetTable<Promotion>(); } }
		public ITable<PromotionList>    PromotionLists    { get { return this.GetTable<PromotionList>(); } }
		public ITable<Subscription>     Subscriptions     { get { return this.GetTable<Subscription>(); } }
		public ITable<SubscriptionType> SubscriptionTypes { get { return this.GetTable<SubscriptionType>(); } }

		partial void InitMappingSchema()
		{
		}

		public PostgresDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PostgresDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PostgresDB(DataOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PostgresDB(DataOptions<PostgresDB> options)
			: base(options.Options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="FoodService", Name="Customer")]
	public partial class Customer
	{
		[PrimaryKey, Identity   ] public int    CustomerID    { get; set; } // integer
		[Column,        Nullable] public string Name          { get; set; } // character varying
		[Column,        Nullable] public string Email         { get; set; } // character varying
		[Column,        Nullable] public string LastAddress   { get; set; } // character varying
		[Column,     NotNull    ] public string PhoneNumber   { get; set; } // text
		[Column,        Nullable] public int?   PromotionList { get; set; } // integer

		#region Associations

		/// <summary>
		/// CustomerID_FK_BackReference (FoodService.Subscription)
		/// </summary>
		[Association(ThisKey="CustomerID", OtherKey="CustomerID", CanBeNull=true)]
		public IEnumerable<Subscription> CustomerIDFkBackReferences { get; set; }

		/// <summary>
		/// CustomerID_FK_BackReference (FoodService.Order)
		/// </summary>
		[Association(ThisKey="CustomerID", OtherKey="CustomerID", CanBeNull=true)]
		public IEnumerable<Order> CustomerIdfks { get; set; }

		/// <summary>
		/// CustomerID_FK_BackReference (FoodService.PromotionList)
		/// </summary>
		[Association(ThisKey="CustomerID", OtherKey="CustomerID", CanBeNull=true)]
		public IEnumerable<PromotionList> FKCustomerIDFkBackReferences { get; set; }

		#endregion
	}

	[Table(Schema="FoodService", Name="DeliveryPoint")]
	public partial class DeliveryPoint
	{
		[PrimaryKey, Identity] public int    DeliveryPointID { get; set; } // integer
		[Column,     NotNull ] public string Address         { get; set; } // text
		[Column,     NotNull ] public string Name            { get; set; } // text

		#region Associations

		/// <summary>
		/// DeliveryPointID_FK_BackReference (FoodService.Order)
		/// </summary>
		[Association(ThisKey="DeliveryPointID", OtherKey="DeliveryPointID", CanBeNull=true)]
		public IEnumerable<Order> DeliveryPointIdfks { get; set; }

		#endregion
	}

	[Table(Schema="FoodService", Name="Food")]
	public partial class Food
	{
		[PrimaryKey, Identity   ] public int     FoodID      { get; set; } // integer
		[Column,     NotNull    ] public string  FoodName    { get; set; } // text
		[Column,     NotNull    ] public decimal Price       { get; set; } // money
		[Column,        Nullable] public string  Components  { get; set; } // text
		[Column,        Nullable] public string  Description { get; set; } // text

		#region Associations

		/// <summary>
		/// FoodID_FK_BackReference (FoodService.OrderDetails)
		/// </summary>
		[Association(ThisKey="FoodID", OtherKey="FoodItem", CanBeNull=true)]
		public IEnumerable<OrderDetail> FoodIdfks { get; set; }

		#endregion
	}

	[Table(Schema="FoodService", Name="Order")]
	public partial class Order
	{
		[PrimaryKey, Identity   ] public int            OrderID         { get; set; } // integer
		[Column,        Nullable] public int?           PaymentID       { get; set; } // integer
		[Column,     NotNull    ] public int            CustomerID      { get; set; } // integer
		[Column,     NotNull    ] public string         Address         { get; set; } // text
		[Column,     NotNull    ] public DateTimeOffset Time            { get; set; } // timestamp (6) with time zone
		[Column,     NotNull    ] public DateTime       Date            { get; set; } // date
		[Column,        Nullable] public int?           DeliveryPointID { get; set; } // integer

		#region Associations

		/// <summary>
		/// CustomerID_FK (FoodService.Customer)
		/// </summary>
		[Association(ThisKey="CustomerID", OtherKey="CustomerID", CanBeNull=false)]
		public Customer Customer { get; set; }

		/// <summary>
		/// DeliveryPointID_FK (FoodService.DeliveryPoint)
		/// </summary>
		[Association(ThisKey="DeliveryPointID", OtherKey="DeliveryPointID", CanBeNull=true)]
		public DeliveryPoint DeliveryPoint { get; set; }

		/// <summary>
		/// OrderID_FK_BackReference (FoodService.OrderDetails)
		/// </summary>
		[Association(ThisKey="OrderID", OtherKey="OrderID", CanBeNull=true)]
		public IEnumerable<OrderDetail> OrderIdfks { get; set; }

		/// <summary>
		/// PaymentID_FK (FoodService.Payment)
		/// </summary>
		[Association(ThisKey="PaymentID", OtherKey="PaymentID", CanBeNull=true)]
		public Payment Payment { get; set; }

		#endregion
	}

	[Table(Schema="FoodService", Name="OrderDetails")]
	public partial class OrderDetail
	{
		[Column, Nullable] public int? FoodItem { get; set; } // integer
		[Column, Nullable] public int? Quantity { get; set; } // integer
		[Column, Nullable] public int? OrderID  { get; set; } // integer

		#region Associations

		/// <summary>
		/// FoodID_FK (FoodService.Food)
		/// </summary>
		[Association(ThisKey="FoodItem", OtherKey="FoodID", CanBeNull=true)]
		public Food FoodIDFK { get; set; }

		/// <summary>
		/// OrderID_FK (FoodService.Order)
		/// </summary>
		[Association(ThisKey="OrderID", OtherKey="OrderID", CanBeNull=true)]
		public Order Order { get; set; }

		/// <summary>
		/// OrderTemplateID_FK_BackReference (FoodService.SubscriptionType)
		/// </summary>
		[Association(ThisKey="OrderID", OtherKey="OrderTemplate", CanBeNull=true)]
		public IEnumerable<SubscriptionType> OrderTemplateIdfks { get; set; }

		#endregion
	}

	[Table(Schema="FoodService", Name="Payment")]
	public partial class Payment
	{
		[PrimaryKey, Identity] public int            PaymentID     { get; set; } // integer
		[Column,     NotNull ] public decimal        NetPrice      { get; set; } // money
		[Column,     NotNull ] public decimal        PaymentAmount { get; set; } // money
		[Column,     NotNull ] public DateTimeOffset Time          { get; set; } // time with time zone
		[Column,     NotNull ] public DateTime       Date          { get; set; } // date

		#region Associations

		/// <summary>
		/// PaymentID_FK_BackReference (FoodService.Subscription)
		/// </summary>
		[Association(ThisKey="PaymentID", OtherKey="PaymentID", CanBeNull=true)]
		public IEnumerable<Subscription> PaymentIDFkBackReferences { get; set; }

		/// <summary>
		/// PaymentID_FK_BackReference (FoodService.Order)
		/// </summary>
		[Association(ThisKey="PaymentID", OtherKey="PaymentID", CanBeNull=true)]
		public IEnumerable<Order> PaymentIdfks { get; set; }

		#endregion
	}

	[Table(Schema="FoodService", Name="Promotion")]
	public partial class Promotion
	{
		[PrimaryKey, Identity] public int    PromotionID { get; set; } // integer
		[Column,     NotNull ] public int    Discount    { get; set; } // integer
		[Column,     NotNull ] public string Name        { get; set; } // text

		#region Associations

		/// <summary>
		/// PromotionID_FK_BackReference (FoodService.PromotionList)
		/// </summary>
		[Association(ThisKey="PromotionID", OtherKey="PromotionID", CanBeNull=true)]
		public IEnumerable<PromotionList> PromotionIdfks { get; set; }

		#endregion
	}

	[Table(Schema="FoodService", Name="PromotionList")]
	public partial class PromotionList
	{
		[Column, Nullable] public int? CustomerID  { get; set; } // integer
		[Column, Nullable] public int? PromotionID { get; set; } // integer

		#region Associations

		/// <summary>
		/// CustomerID_FK (FoodService.Customer)
		/// </summary>
		[Association(ThisKey="CustomerID", OtherKey="CustomerID", CanBeNull=true)]
		public Customer Customer { get; set; }

		/// <summary>
		/// PromotionID_FK (FoodService.Promotion)
		/// </summary>
		[Association(ThisKey="PromotionID", OtherKey="PromotionID", CanBeNull=true)]
		public Promotion Promotion { get; set; }

		#endregion
	}

	[Table(Schema="FoodService", Name="Subscription")]
	public partial class Subscription
	{
		[PrimaryKey, Identity] public int       SubscriptionID   { get; set; } // integer
		[Column,     Nullable] public DateTime? DateStart        { get; set; } // date
		[Column,     Nullable] public DateTime? DateEnd          { get; set; } // date
		[Column,     Nullable] public int?      CustomerID       { get; set; } // integer
		[Column,     Nullable] public int?      PaymentID        { get; set; } // integer
		[Column,     Nullable] public int?      SubscriptionType { get; set; } // integer

		#region Associations

		/// <summary>
		/// CustomerID_FK (FoodService.Customer)
		/// </summary>
		[Association(ThisKey="CustomerID", OtherKey="CustomerID", CanBeNull=true)]
		public Customer Customer { get; set; }

		/// <summary>
		/// PaymentID_FK (FoodService.Payment)
		/// </summary>
		[Association(ThisKey="PaymentID", OtherKey="PaymentID", CanBeNull=true)]
		public Payment Payment { get; set; }

		/// <summary>
		/// SubscriptionTypeID_FK (FoodService.SubscriptionType)
		/// </summary>
		[Association(ThisKey="SubscriptionType", OtherKey="SubscriptionTypeID", CanBeNull=true)]
		public SubscriptionType SubscriptionTypeIDFK { get; set; }

		#endregion
	}

	[Table(Schema="FoodService", Name="SubscriptionType")]
	public partial class SubscriptionType
	{
		[PrimaryKey, Identity] public int    SubscriptionTypeID { get; set; } // integer
		[Column,     Nullable] public string Description        { get; set; } // text
		[Column,     Nullable] public int?   OrderTemplate      { get; set; } // integer

		#region Associations

		/// <summary>
		/// OrderTemplateID_FK (FoodService.OrderDetails)
		/// </summary>
		[Association(ThisKey="OrderTemplate", OtherKey="OrderID", CanBeNull=true)]
		public OrderDetail OrderTemplateIDFK { get; set; }

		/// <summary>
		/// SubscriptionTypeID_FK_BackReference (FoodService.Subscription)
		/// </summary>
		[Association(ThisKey="SubscriptionTypeID", OtherKey="SubscriptionType", CanBeNull=true)]
		public IEnumerable<Subscription> SubscriptionTypeIdfks { get; set; }

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Customer Find(this ITable<Customer> table, int CustomerID)
		{
			return table.FirstOrDefault(t =>
				t.CustomerID == CustomerID);
		}

		public static DeliveryPoint Find(this ITable<DeliveryPoint> table, int DeliveryPointID)
		{
			return table.FirstOrDefault(t =>
				t.DeliveryPointID == DeliveryPointID);
		}

		public static Food Find(this ITable<Food> table, int FoodID)
		{
			return table.FirstOrDefault(t =>
				t.FoodID == FoodID);
		}

		public static Order Find(this ITable<Order> table, int OrderID)
		{
			return table.FirstOrDefault(t =>
				t.OrderID == OrderID);
		}

		public static Payment Find(this ITable<Payment> table, int PaymentID)
		{
			return table.FirstOrDefault(t =>
				t.PaymentID == PaymentID);
		}

		public static Promotion Find(this ITable<Promotion> table, int PromotionID)
		{
			return table.FirstOrDefault(t =>
				t.PromotionID == PromotionID);
		}

		public static Subscription Find(this ITable<Subscription> table, int SubscriptionID)
		{
			return table.FirstOrDefault(t =>
				t.SubscriptionID == SubscriptionID);
		}

		public static SubscriptionType Find(this ITable<SubscriptionType> table, int SubscriptionTypeID)
		{
			return table.FirstOrDefault(t =>
				t.SubscriptionTypeID == SubscriptionTypeID);
		}
	}
}
