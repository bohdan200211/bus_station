CREATE VIEW [dbo].[Fulluser]
	AS select
	u.id as id, u.username as username, u.password as password, 
	p.lastname as lastname, p.firstname as firstname, 
	u.createAt as createAt 
	from [User] u 
	inner join [Profile] p 
	on 
	u.id = p.id;
