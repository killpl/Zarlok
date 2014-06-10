
CREATE TABLE Profile  (
	id 			int IDENTITY PRIMARY KEY,
	login		varchar(20) NOT NULL,
	password	varchar(50) NOT NULL,
	firstname	varchar(100),
	surname		varchar(100),
	type 		tinyint
);


CREATE TABLE Category (
	id 		int IDENTITY PRIMARY KEY,
	name	varchar(100)	
);


CREATE TABLE Food (
	id 			int IDENTITY PRIMARY KEY,
	name		varchar(100),
	calories	int,
	categoryId	int REFERENCES Category(id)
);



CREATE TABLE Eaten (
	id			int IDENTITY PRIMARY KEY,
	foodId		int REFERENCES Food(id),
	profileId	int REFERENCES Profile(id),
	date		datetime DEFAULT current_timestamp
);


CREATE TABLE Achievement (
	id 			int IDENTITY PRIMARY KEY,
	name		varchar(100),
	icon		varchar(200)
);


CREATE TABLE ProfileAchievement (
	id 				int IDENTITY PRIMARY KEY,
	profileId		int REFERENCES Profile(id),
	achievementId	int REFERENCES Achievement(id),
	date			datetime DEFAULT current_timestamp
);


CREATE TABLE Friendship (
	id 			int IDENTITY PRIMARY KEY,
	sender		int REFERENCES Profile(id), 
	receiver	int REFERENCES Profile(id)
);


CREATE TABLE Request (
	id 			int IDENTITY PRIMARY KEY,
	sender		int REFERENCES Profile(id), 
	receiver	int REFERENCES Profile(id),
	sent		datetime DEFAULT current_timestamp
); 


CREATE TABLE Identifier (
	id			int IDENTITY PRIMARY KEY,
	keyIdentifier 			varchar(100),
	profileId	int REFERENCES Profile(id),
	date		datetime DEFAULT current_timestamp

);
