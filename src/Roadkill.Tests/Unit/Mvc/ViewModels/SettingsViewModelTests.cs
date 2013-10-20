﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Roadkill.Core.Configuration;
using Roadkill.Core.Database;
using Roadkill.Core.Mvc.ViewModels;
using Roadkill.Core.Plugins;
using Roadkill.Tests.Unit.StubsAndMocks;

namespace Roadkill.Tests.Unit
{
	[TestFixture]
	[Category("Unit")]
	public class SettingsViewModelTests
	{
		[Test]
		public void Constructor_Should_Convert_ApplicationSettings_And_SiteSettings_To_Properties()
		{
			// Arrange
			ApplicationSettings appSettings = new ApplicationSettings()
			{
				AdminRoleName = "admin role name",
				AttachmentsFolder = @"c:\AttachmentsFolder",
				UseObjectCache = true,
				UseBrowserCache = true,
				ConnectionString = "connection string",
				DataStoreType = DataStoreType.SqlServer2008,
				EditorRoleName = "editor role name",
				LdapConnectionString = "ldap connection string",
				LdapUsername = "ldap username",
				LdapPassword = "ldap password",
				UseWindowsAuthentication = true
			};

			SiteSettings siteSettings = new SiteSettings()
			{
				AllowedFileTypes = "jpg,png,gif",
				AllowUserSignup = true,
				IsRecaptchaEnabled = true,
				MarkupType = "markuptype",
				RecaptchaPrivateKey = "privatekey",
				RecaptchaPublicKey = "publickey",
				SiteName = "sitename",
				SiteUrl = "siteurl",
				Theme = "theme",
				OverwriteExistingFiles = true,
				HeadContent = "head content",
				MenuMarkup = "menu markup"
			};

			
			// Act
			SettingsViewModel model = new SettingsViewModel(appSettings, siteSettings);

			// Assert
			Assert.That(model.AdminRoleName, Is.EqualTo(appSettings.AdminRoleName));
			Assert.That(model.AttachmentsFolder, Is.EqualTo(appSettings.AttachmentsFolder));
			Assert.That(model.UseObjectCache, Is.EqualTo(appSettings.UseObjectCache));
			Assert.That(model.UseBrowserCache, Is.EqualTo(appSettings.UseBrowserCache));
			Assert.That(model.ConnectionString, Is.EqualTo(appSettings.ConnectionString));
			Assert.That(model.DataStoreTypeName, Is.EqualTo(appSettings.DataStoreType.Name));
			Assert.That(model.EditorRoleName, Is.EqualTo(appSettings.EditorRoleName));
			Assert.That(model.LdapConnectionString, Is.EqualTo(appSettings.LdapConnectionString));
			Assert.That(model.LdapUsername, Is.EqualTo(appSettings.LdapUsername));
			Assert.That(model.LdapPassword, Is.EqualTo(appSettings.LdapPassword));
			Assert.That(model.UseWindowsAuth, Is.EqualTo(appSettings.UseWindowsAuthentication));

			Assert.That(model.AllowedFileTypes, Is.EqualTo(siteSettings.AllowedFileTypes));
			Assert.That(model.AllowUserSignup, Is.EqualTo(siteSettings.AllowUserSignup));
			Assert.That(model.IsRecaptchaEnabled, Is.EqualTo(siteSettings.IsRecaptchaEnabled));
			Assert.That(model.MarkupType, Is.EqualTo(siteSettings.MarkupType));
			Assert.That(model.RecaptchaPrivateKey, Is.EqualTo(siteSettings.RecaptchaPrivateKey));
			Assert.That(model.RecaptchaPublicKey, Is.EqualTo(siteSettings.RecaptchaPublicKey));
			Assert.That(model.SiteName, Is.EqualTo(siteSettings.SiteName));
			Assert.That(model.SiteUrl, Is.EqualTo(siteSettings.SiteUrl));
			Assert.That(model.Theme, Is.EqualTo(siteSettings.Theme));
			Assert.That(model.OverwriteExistingFiles, Is.EqualTo(siteSettings.OverwriteExistingFiles));
			Assert.That(model.HeadContent, Is.EqualTo(siteSettings.HeadContent));
			Assert.That(model.MenuMarkup, Is.EqualTo(siteSettings.MenuMarkup));
		}

		[Test]
		public void Constructor_Should_Remove_Spaces_From_SiteSettings_Allow_File_Types()
		{
			// Arrange
			ApplicationSettings appSettings = new ApplicationSettings();
			SiteSettings siteSettings = new SiteSettings()
			{
				AllowedFileTypes = "jpg, png,      gif"
			};


			// Act
			SettingsViewModel model = new SettingsViewModel(appSettings, siteSettings);

			// Assert
			Assert.That(model.AllowedFileTypes, Is.EqualTo("jpg,png,gif"));
		}

		[Test]
		public void FillFromApplicationSettings_Should_Convert_ApplicationSettings_To_Properties()
		{
			// Arrange
			ApplicationSettings appSettings = new ApplicationSettings()
			{
				AdminRoleName = "admin role name",
				AttachmentsFolder = @"c:\AttachmentsFolder",
				UseObjectCache = true,
				UseBrowserCache = true,
				ConnectionString = "connection string",
				DataStoreType = DataStoreType.SqlServer2008,
				EditorRoleName = "editor role name",
				LdapConnectionString = "ldap connection string",
				LdapUsername = "ldap username",
				LdapPassword = "ldap password",
				UseWindowsAuthentication = true
			};

			// Act
			SettingsViewModel model = new SettingsViewModel();
			model.FillFromApplicationSettings(appSettings);

			// Assert
			Assert.That(model.AdminRoleName, Is.EqualTo(appSettings.AdminRoleName));
			Assert.That(model.AttachmentsFolder, Is.EqualTo(appSettings.AttachmentsFolder));
			Assert.That(model.UseObjectCache, Is.EqualTo(appSettings.UseObjectCache));
			Assert.That(model.UseBrowserCache, Is.EqualTo(appSettings.UseBrowserCache));
			Assert.That(model.ConnectionString, Is.EqualTo(appSettings.ConnectionString));
			Assert.That(model.DataStoreTypeName, Is.EqualTo(appSettings.DataStoreType.Name));
			Assert.That(model.EditorRoleName, Is.EqualTo(appSettings.EditorRoleName));
			Assert.That(model.LdapConnectionString, Is.EqualTo(appSettings.LdapConnectionString));
			Assert.That(model.LdapUsername, Is.EqualTo(appSettings.LdapUsername));
			Assert.That(model.LdapPassword, Is.EqualTo(appSettings.LdapPassword));
			Assert.That(model.UseWindowsAuth, Is.EqualTo(appSettings.UseWindowsAuthentication));
		}
	}
}