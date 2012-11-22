﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Roadkill.Core.Configuration;
using Roadkill.Core.Converters;

namespace Roadkill.Core
{
	/// <summary>
	/// Contains versioned text data for a page for use with the NHibernate data store. This object is intended for internal use only.
	/// </summary>
	public class PageContent
	{
		public virtual Guid Id { get; set; }
		public virtual Page Page { get; set; }
		public virtual string Text { get; set; }
		public virtual string EditedBy { get; set; }
		public virtual DateTime EditedOn { get; set; }
		public virtual int VersionNumber { get; set; }

		public virtual PageSummary ToSummary(MarkupConverter markupConverter)
		{
			return new PageSummary()
			{
				Id = Page.Id,
				Title = Page.Title,
				PreviousTitle = Page.Title,
				CreatedBy = Page.CreatedBy,
				CreatedOn = Page.CreatedOn,
				IsLocked = Page.IsLocked,
				ModifiedBy = Page.ModifiedBy,
				ModifiedOn = Page.ModifiedOn,
				Tags = Page.Tags,
				Content = Text,
				ContentAsHtml = markupConverter.ToHtml(Text),
				VersionNumber = VersionNumber,
			};
		}
	}

	/// <summary>
	/// Configures the Fluent NHibernate mapping for a <see cref="PageContent"/>
	/// </summary>
	public class PageContentMap : ClassMap<PageContent>
	{
		public PageContentMap()
		{
			Table("roadkill_pagecontent");
			Id(x => x.Id);
			Map(x => x.EditedBy);
			Map(x => x.EditedOn);
			Map(x => x.VersionNumber);

			// nvarchar(max) is now the recommended way of storing text in SQL Server
			PropertyPart part = Map(x => x.Text).CustomType("StringClob").Length(Int16.MaxValue);
			
			// Setting LazyLoad when the L2Cache is enabled makes it grab the data
			// from the database for each request regardless.
			if (!RoadkillSettings.GetInstance().ApplicationSettings.CachedEnabled || !RoadkillSettings.GetInstance().ApplicationSettings.CacheText)
				part.LazyLoad();

			References<Page>(x => x.Page)
				.Column("pageid")
				.Cascade
				.None().Index("pageId");

			Cache.ReadWrite().IncludeAll();
		}
	}
}
