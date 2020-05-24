-- select-customer-lastname-beginswiths.sql
-- Selects all customers with the last name that begins with the letter S

SELECT
	CustID,
	LastName,
	FirstName
FROM Customer (NOLOCK) 
WHERE LastName LIKE 'S%'
ORDER BY LastName DESC, FirstName DESC
