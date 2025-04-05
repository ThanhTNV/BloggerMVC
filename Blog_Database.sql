Use master;
DROP DATABASE BLOGGER_V1;
CREATE DATABASE BLOGGER_V1
GO
USE BLOGGER_V1
GO

CREATE TABLE [User]
(
  Username VARCHAR(25) NOT NULL,
  [Password] VARCHAR(25) NOT NULL,
  [Status] INT DEFAULT(1) NOT NULL,
  DateOfBirth DATE NOT NULL,
  CONSTRAINT PK_User_Username PRIMARY KEY (Username),
  CONSTRAINT CHK_User_DateOfBirth CHECK (ABS(DATEDIFF(year, GETDATE(), DateOfBirth)) BETWEEN 14 AND 99),
  CONSTRAINT CHK_User_Status CHECK(Status IN (1,2,3)) -- Inactive(Cannot post blog), Active(Can post blog), Banned(Cannot login)
);


CREATE TABLE Category
(
  CategoryId INT IDENTITY(1,1) NOT NULL,
  CategoryName NVARCHAR(100) NOT NULL,
  CONSTRAINT PK_Category_CategoryId PRIMARY KEY (CategoryId),
  CONSTRAINT UNI_Category_CategoryName UNIQUE (CategoryName)
);

CREATE TABLE Blog
(
  BlogId INT IDENTITY(1,1) NOT NULL,
  BlogTitle NVARCHAR(100) NOT NULL,
  CreatedAt DATETIME DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
  BlogSummary NVARCHAR(MAX),
  Content NVARCHAR(MAX) NOT NULL,
  [Status] INT DEFAULT(1) NOT NULL,
  CategoryId INT NOT NULL,
  Username VARCHAR(25),
  CONSTRAINT PK_Blog_BlogId PRIMARY KEY (BlogId),
  CONSTRAINT UNI_Blog_BlogTitle UNIQUE (BlogTitle),
  CONSTRAINT FK_Blog_CategoryId FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId) ON DELETE CASCADE,
  CONSTRAINT FK_Blog_Username FOREIGN KEY (Username) REFERENCES [User](Username) ON DELETE SET NULL,
  CONSTRAINT CHK_Blog_Status CHECK([Status] in (1, 2, 3)) -- Pending, Approved, Denied, Banned
);

CREATE INDEX IX_Blog_BlogTitle ON Blog (BlogTitle);

CREATE TABLE BlogReaction
(
  BlogId INT NOT NULL,
  Username VARCHAR(25) NOT NULL,
  ReactedAt DATETIME DEFAULT(CURRENT_TIMESTAMP) NOT NULL
  CONSTRAINT PK_BlogReaction_BlogId_Username PRIMARY KEY (BlogId, Username),
  CONSTRAINT FK_BlogReaction_BlogId FOREIGN KEY (BlogId) REFERENCES Blog(BlogId) ON DELETE CASCADE,
  CONSTRAINT FK_BlogReaction_Username FOREIGN KEY (Username) REFERENCES [User](Username) ON DELETE CASCADE
);

CREATE TABLE Comment
(
  CommentId INT IDENTITY(1,1) NOT NULL,
  [Value] VARCHAR(MAX) NOT NULL,
  CommentedAt DATETIME DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
  BlogId INT NOT NULL,
  Username VARCHAR(25) NOT NULL,
  ParentId INT,
  CONSTRAINT PK_Comment_CommentId PRIMARY KEY (CommentId),
  CONSTRAINT FK_Comment_BlogId FOREIGN KEY (BlogId) REFERENCES Blog(BlogId) ON DELETE CASCADE,
  CONSTRAINT FK_Comment_Username FOREIGN KEY (Username) REFERENCES [User](Username) ON DELETE CASCADE,
  CONSTRAINT FK_Comment_ParentId FOREIGN KEY (ParentId) REFERENCES Comment(CommentId) ON DELETE NO ACTION
)
GO

CREATE TABLE CommentReaction
(
  CommentId INT NOT NULL,
  Username VARCHAR(25) NOT NULL,
  ReactedAt DATETIME DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
  CONSTRAINT PK_CommentReaction_CommentId_Username PRIMARY KEY (CommentId, Username),
  CONSTRAINT FK_CommentReaction_CommentId FOREIGN KEY (CommentId) REFERENCES Comment(CommentId) ON DELETE CASCADE,
  CONSTRAINT FK_CommentReaction_Username FOREIGN KEY (Username) REFERENCES [User](Username) ON DELETE NO ACTION
)
GO

CREATE TRIGGER TR_DeleteChildComments
ON Comment
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH CommentsToDelete AS (
         -- Start with the deleted comments
         SELECT CommentId
         FROM deleted
         UNION ALL
         -- Recursively find all child comments
         SELECT C.CommentId
         FROM Comment C
         INNER JOIN CommentsToDelete CTD ON C.ParentId = CTD.CommentId
    )
    DELETE FROM Comment
    WHERE CommentId IN (SELECT CommentId FROM CommentsToDelete);
END;

-- Insert sample users
INSERT INTO [User] (Username, [Password], [Status], DateOfBirth)
VALUES
  ('alice', 'password1', 2, '1985-03-15'),   -- Age ≈40 (active)
  ('bob', 'password2', 2, '1990-07-22'),     -- Age ≈35 (active)
  ('charlie', 'password3', 1, '2008-01-10'),  -- Age ≈17 (inactive)
  ('dave', 'password4', 3, '1975-11-30'),     -- Age ≈50 (banned)
  ('eve', 'password5', 2, '1960-05-05');      -- Age ≈65 (active)

-- Insert sample categories
INSERT INTO Category (CategoryName)
VALUES
  ('Technology'),
  ('Lifestyle'),
  ('Travel'),
  ('Food'),
  ('Education');

-- Insert sample blogs
-- (Assuming CategoryId values 1 through 5 exist and using valid usernames)
INSERT INTO Blog (BlogTitle, BlogSummary, Content, [Status], CategoryId, Username)
VALUES
  ('Tech Trends 2025', 'A look into upcoming tech trends', 'Content of Tech Trends 2025...', 2, 1, 'alice'),
  ('Healthy Living Tips', 'Guide to a healthy lifestyle', 'Content of Healthy Living Tips...', 2, 2, 'bob'),
  ('Backpacking Europe', 'Experiences while backpacking through Europe', 'Content of Backpacking Europe...', 1, 3, 'charlie'),
  ('Gourmet Recipes', 'Delicious recipes from around the world', 'Content of Gourmet Recipes...', 2, 4, 'dave'),
  ('Online Learning', 'The future of education in the digital age', 'Content of Online Learning...', 3, 5, 'eve');

-- Insert sample blog reactions
-- (Using existing BlogId values and valid usernames)

SELECT * FROM Blog
INSERT INTO BlogReaction (BlogId, Username)
VALUES
  (1, 'bob'),
  (1, 'charlie'),
  (3, 'alice'),
  (4, 'dave'),
  (5, 'alice');

-- Insert sample comments
-- For a child comment, we assume the first comment gets CommentId = 1.
INSERT INTO Comment ([Value], BlogId, Username, ParentId)
VALUES
  ('Great blog post!', 2, 'charlie', NULL),                -- CommentId 1 (top-level)
  ('I completely agree with your point.',2, 'dave', NULL),     -- Comment 2
  ('Very informative, thanks!', 3, 'eve', NULL),             -- CommentId 3
  ('I have a question about this topic.', 4, 'alice', NULL), -- CommentId 4
  ('Thanks for sharing!', 5, 'bob', NULL);                    -- CommentId 5

-- Insert sample comment reactions
INSERT INTO CommentReaction (CommentId, Username)
VALUES
  (1, 'alice'),
  (1, 'bob'),
  (3, 'charlie'),
  (4, 'eve'),
  (5, 'dave');