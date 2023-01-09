IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AppRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AppRoleClaims] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppRoles] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NULL,
    [ModifiedOn] datetime2 NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [IsInBuilt] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [NormalizedName] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AppRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AppUserRoles] PRIMARY KEY ([UserId], [RoleId])
);
GO

CREATE TABLE [AppUserTokenMap] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserTokenMap] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [OpenIddictApplications] (
    [Id] uniqueidentifier NOT NULL,
    [AppId] nvarchar(max) NOT NULL,
    [ClientId] nvarchar(100) NULL,
    [ClientSecret] nvarchar(max) NULL,
    [ConcurrencyToken] nvarchar(50) NULL,
    [ConsentType] nvarchar(50) NULL,
    [DisplayName] nvarchar(max) NULL,
    [DisplayNames] nvarchar(max) NULL,
    [Permissions] nvarchar(max) NULL,
    [PostLogoutRedirectUris] nvarchar(max) NULL,
    [Properties] nvarchar(max) NULL,
    [RedirectUris] nvarchar(max) NULL,
    [Requirements] nvarchar(max) NULL,
    [Type] nvarchar(50) NULL,
    CONSTRAINT [PK_OpenIddictApplications] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OpenIddictScopes] (
    [Id] uniqueidentifier NOT NULL,
    [ConcurrencyToken] nvarchar(50) NULL,
    [Description] nvarchar(max) NULL,
    [Descriptions] nvarchar(max) NULL,
    [DisplayName] nvarchar(max) NULL,
    [DisplayNames] nvarchar(max) NULL,
    [Name] nvarchar(200) NULL,
    [Properties] nvarchar(max) NULL,
    [Resources] nvarchar(max) NULL,
    CONSTRAINT [PK_OpenIddictScopes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [Id] int NOT NULL IDENTITY,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey])
);
GO

CREATE TABLE [UserModel] (
    [Id] uniqueidentifier NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [Gender] int NOT NULL,
    [Activated] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [ModifiedBy] nvarchar(max) NOT NULL,
    [IsPasswordDefault] bit NOT NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_UserModel] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [visitors] (
    [Id] uniqueidentifier NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [Nationality] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [PurposeOfEntry] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [ImageName] nvarchar(max) NOT NULL,
    [ImageFileSize] bigint NOT NULL,
    [ImageOriginalFileName] nvarchar(max) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [CreatedBy] nvarchar(max) NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [DeletedBy] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_visitors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OpenIddictAuthorizations] (
    [Id] uniqueidentifier NOT NULL,
    [ApplicationId] uniqueidentifier NULL,
    [ConcurrencyToken] nvarchar(50) NULL,
    [CreationDate] datetime2 NULL,
    [Properties] nvarchar(max) NULL,
    [Scopes] nvarchar(max) NULL,
    [Status] nvarchar(50) NULL,
    [Subject] nvarchar(400) NULL,
    [Type] nvarchar(50) NULL,
    CONSTRAINT [PK_OpenIddictAuthorizations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [OpenIddictApplications] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [OpenIddictTokens] (
    [Id] uniqueidentifier NOT NULL,
    [ApplicationId] uniqueidentifier NULL,
    [AuthorizationId] uniqueidentifier NULL,
    [ConcurrencyToken] nvarchar(50) NULL,
    [CreationDate] datetime2 NULL,
    [ExpirationDate] datetime2 NULL,
    [Payload] nvarchar(max) NULL,
    [Properties] nvarchar(max) NULL,
    [RedemptionDate] datetime2 NULL,
    [ReferenceId] nvarchar(100) NULL,
    [Status] nvarchar(50) NULL,
    [Subject] nvarchar(400) NULL,
    [Type] nvarchar(50) NULL,
    CONSTRAINT [PK_OpenIddictTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OpenIddictTokens_OpenIddictApplications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [OpenIddictApplications] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId] FOREIGN KEY ([AuthorizationId]) REFERENCES [OpenIddictAuthorizations] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'RoleId') AND [object_id] = OBJECT_ID(N'[AppRoleClaims]'))
    SET IDENTITY_INSERT [AppRoleClaims] ON;
INSERT INTO [AppRoleClaims] ([Id], [ClaimType], [ClaimValue], [RoleId])
VALUES (2, N'Permission', N'FULL_CONTROL', '2d579044-e875-44cc-8e86-2ecbc29208c7');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'RoleId') AND [object_id] = OBJECT_ID(N'[AppRoleClaims]'))
    SET IDENTITY_INSERT [AppRoleClaims] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'CreatedBy', N'CreatedOn', N'IsInBuilt', N'ModifiedBy', N'ModifiedOn', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AppRoles]'))
    SET IDENTITY_INSERT [AppRoles] ON;
INSERT INTO [AppRoles] ([Id], [ConcurrencyStamp], [CreatedBy], [CreatedOn], [IsInBuilt], [ModifiedBy], [ModifiedOn], [Name], [NormalizedName])
VALUES ('2d579044-e875-44cc-8e86-2ecbc29208c7', N'd5e444eddf9a4854bede120ed6d9285e', NULL, NULL, CAST(0 AS bit), NULL, NULL, N'SYS_ADMIN', N'SYS_ADMIN');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'CreatedBy', N'CreatedOn', N'IsInBuilt', N'ModifiedBy', N'ModifiedOn', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AppRoles]'))
    SET IDENTITY_INSERT [AppRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppUserRoles]'))
    SET IDENTITY_INSERT [AppUserRoles] ON;
INSERT INTO [AppUserRoles] ([RoleId], [UserId])
VALUES ('2d579044-e875-44cc-8e86-2ecbc29208c7', '426cc091-7876-4811-b472-35d5cb6bdd9a');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppUserRoles]'))
    SET IDENTITY_INSERT [AppUserRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Activated', N'ConcurrencyStamp', N'CreatedBy', N'CreatedOn', N'Email', N'EmailConfirmed', N'FirstName', N'Gender', N'IsDeleted', N'IsPasswordDefault', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'ModifiedBy', N'ModifiedOn', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[UserModel]'))
    SET IDENTITY_INSERT [UserModel] ON;
INSERT INTO [UserModel] ([Id], [AccessFailedCount], [Activated], [ConcurrencyStamp], [CreatedBy], [CreatedOn], [Email], [EmailConfirmed], [FirstName], [Gender], [IsDeleted], [IsPasswordDefault], [LastName], [LockoutEnabled], [LockoutEnd], [ModifiedBy], [ModifiedOn], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES ('426cc091-7876-4811-b472-35d5cb6bdd9a', 0, CAST(1 AS bit), N'9422de1f-4049-4f40-83f2-953ac46b59f5', N'sholl45@gmail.com', '2023-01-08T00:00:00.0000000', N'sholl45@gmail.com', CAST(1 AS bit), N'John', 0, CAST(0 AS bit), CAST(0 AS bit), N'Doe', CAST(0 AS bit), NULL, N'sholl45@gmail.com', NULL, N'SHOLL45@GMAIL.COM', N'SHOLL45@GMAIL.COM', N'AQAAAAIAAYagAAAAEILeEe8wXAzqDSMg/+eihwLBYOvMtJzLlcLvpbNP9fPB7fpgckBwgjyF0z0+u/U2Ow==', N'09067657843', CAST(1 AS bit), N'ED294048-52E4-4311-A7DA-4C1A862411F6', CAST(0 AS bit), N'sholl45@gmail.com'),
('508eacfc-e092-4b6c-828c-b93f9f77d582', 0, CAST(1 AS bit), N'457477ab-f41d-4bfc-bb39-395de9c5bde6', N'leno78@gmail.com', '2023-01-08T00:00:00.0000000', N'leno78@gmail.com', CAST(1 AS bit), N'Prolifik', 0, CAST(0 AS bit), CAST(0 AS bit), N'Lexzy', CAST(0 AS bit), NULL, N'leno78@gmail.com', NULL, N'LENO78@GMAIL.COM', N'LENO78@GMAIL.COM', N'AQAAAAIAAYagAAAAEAlo0+mr8WWe5hYrkmpXv2tx6xFYzdxtjDetIg/qjJwyKnRzi49p5OYFHaN4zHV/tg==', N'07056543521', CAST(1 AS bit), N'7EAE80E0-137E-4318-8300-5C076F5AFC24', CAST(0 AS bit), N'leno78@gmail.com');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Activated', N'ConcurrencyStamp', N'CreatedBy', N'CreatedOn', N'Email', N'EmailConfirmed', N'FirstName', N'Gender', N'IsDeleted', N'IsPasswordDefault', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'ModifiedBy', N'ModifiedOn', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[UserModel]'))
    SET IDENTITY_INSERT [UserModel] OFF;
GO

CREATE UNIQUE INDEX [IX_OpenIddictApplications_ClientId] ON [OpenIddictApplications] ([ClientId]) WHERE [ClientId] IS NOT NULL;
GO

CREATE INDEX [IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type] ON [OpenIddictAuthorizations] ([ApplicationId], [Status], [Subject], [Type]);
GO

CREATE UNIQUE INDEX [IX_OpenIddictScopes_Name] ON [OpenIddictScopes] ([Name]) WHERE [Name] IS NOT NULL;
GO

CREATE INDEX [IX_OpenIddictTokens_ApplicationId_Status_Subject_Type] ON [OpenIddictTokens] ([ApplicationId], [Status], [Subject], [Type]);
GO

CREATE INDEX [IX_OpenIddictTokens_AuthorizationId] ON [OpenIddictTokens] ([AuthorizationId]);
GO

CREATE UNIQUE INDEX [IX_OpenIddictTokens_ReferenceId] ON [OpenIddictTokens] ([ReferenceId]) WHERE [ReferenceId] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230109163603_init', N'7.0.0');
GO

COMMIT;
GO

