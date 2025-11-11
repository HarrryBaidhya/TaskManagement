


CREATE or alter PROCEDURE spUpdateOrder
    @OrderID INT,
    @CustomerName NVARCHAR(200)=null,
    @CustomerEmail NVARCHAR(200)=null,
    @OrderStatus NVARCHAR(100)=null,
    @Quantity INT=null,
    @ProductID INT=null,
    @SubTotal DECIMAL(18,2)=null
AS
BEGIN
    UPDATE [Order]
    SET 
        CustomerName = @CustomerName,
        CustomerEmail = @CustomerEmail,
        OrderStatus = @OrderStatus
    WHERE OrderID = @OrderID;

    -- Check if the update affected any row
    IF @@ROWCOUNT > 0
        RETURN 1001;  -- Success code
    ELSE
        RETURN 0; 

END