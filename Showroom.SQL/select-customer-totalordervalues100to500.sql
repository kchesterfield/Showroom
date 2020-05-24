-- select-customer-totalordervalues100to500.sql
-- Total value of items ordered, grouped by customers, from 100 to 500

SELECT
	a.CustID,
	a.LastName,
	a.FirstName,
	a.TotalCost
FROM (
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
		CustID) AS a
WHERE
	TotalCost > 100
	AND TotalCost < 500
ORDER BY 
	LastName DESC, 
	FirstName DESC
