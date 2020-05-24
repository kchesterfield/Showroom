-- select-customer-totalordervalues.sql
-- Total value of items ordered, grouped by customers

SELECT
	cu.CustID,
	cu.LastName,
	cu.FirstName,
	SUM(Cost) as [TotalCost]
FROM Customer cu (NOLOCK)
	INNER JOIN [Order] od (NOLOCK) ON od.CustomerID = cu.CustID
	INNER JOIN OrderLine ol (NOLOCK) ON ol.OrdID = od.OrderID
WHERE 
	od.OrderDate > DATEADD(m, -6, GETDATE())
GROUP BY
	CustID
ORDER BY 
	LastName DESC, 
	FirstName DESC
