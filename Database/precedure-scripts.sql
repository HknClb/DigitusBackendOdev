create procedure spGetSuccessfullyRegisteredUsersCount
as
begin
select COUNT(*) as 'SuccessfullyRegisteredUsersCount' from AspNetUsers users where users.EmailConfirmed = 1
end

go

create procedure spGetEmailUnconfirmedUsersCount
as
begin
select COUNT(*) as 'EmailUnconfirmedUsersCount' from AccountVerifications av 
where av.CreatedDate > GETUTCDATE() and av.IsActive = 1 and av.ConfirmationType = 0
end

go

create procedure spGetAverageConfirmationSeconds
@targetDate datetime2(7)
as
begin
select SUM(DATEDIFF(SECOND, av.CreatedDate, av.UpdatedDate)) / COUNT(*) as 'AvarageConfirmationSeconds' from AccountVerifications av
where av.CreatedDate < GETUTCDATE() and av.IsActive = 0 and av.ConfirmationType = 0
and YEAR(av.CreatedDate) = YEAR(@targetDate) and MONTH(av.CreatedDate) = MONTH(@targetDate) and DAY(av.CreatedDate) = DAY(@targetDate)
end