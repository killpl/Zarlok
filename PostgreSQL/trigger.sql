CREATE TRIGGER AchievementTrigger1 ON Eaten
AFTER INSERT
AS
BEGIN
SET NOCOUNT ON
	IF ( (SELECT count(*) FROM Eaten e JOIN inserted i ON e.profileId=i.profileId) = 1) 
	BEGIN
		DECLARE @user_id INT
		SELECT
			@user_id = inserted.profileId
		FROM
			inserted

		INSERT INTO ProfileAchievement (
			profileId, achievementId, date
		) VALUES (
			@user_id, 1, CURRENT_TIMESTAMP
		)
	END
END


CREATE TRIGGER AchievementTrigger2 ON Eaten
AFTER INSERT
AS
BEGIN
SET NOCOUNT ON
	IF ( (SELECT count(*) FROM Eaten e JOIN inserted i ON e.profileId=i.profileId) = 3) 
	BEGIN
		DECLARE @user_id INT
		SELECT
			@user_id = inserted.profileId
		FROM
			inserted

		INSERT INTO ProfileAchievement (
			profileId, achievementId, date
		) VALUES (
			@user_id, 2, CURRENT_TIMESTAMP
		)
	END
END