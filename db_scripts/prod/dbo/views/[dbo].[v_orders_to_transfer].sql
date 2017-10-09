SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                -- =============================================
                -- Author:    Thordur Oskarsson
                -- Create date: 27.01.2015.TO
                -- Description: Return orders to transfer
                -- =============================================
                CREATE VIEW [dbo].[v_orders_to_transfer]
                AS

                   SELECT DISTINCT o.id AS order_id,
                    it.article_no as item_no,
                    loc.location_no,
                    vendloc.location_no AS orderfrom_location_no,
                    isnull(it.color, '') AS color,
                    isnull(it.size, '') AS size,
                    isnull(it.style, '') AS style,
                    o.[user_id],
                    ol.unit_qty_chg,
                    o.est_delivery_date,
                    'purchase' as order_type
                    FROM dbo.orders o
                    INNER JOIN dbo.order_lines ol ON o.id = ol.order_id
                    INNER JOIN dbo.locations vendloc ON o.order_from_location_id = vendloc.id
                    INNER JOIN dbo.locations loc ON loc.id = o.location_id
                    INNER JOIN dbo.items it ON it.id = ol.item_id
                    WHERE o.[status] = 1 AND ol.unit_qty_chg > 0 AND o.deleted = 0


                    /*   AX 365 Version below */
                    /*                    ALTER VIEW [dbo].[v_orders_to_transfer]
                                  				AS
                                  				   SELECT DISTINCT o.id AS order_id,
                                  					it.article_no,
                                  					loc.location_no,
                                  					vendloc.location_no AS orderfrom_location_no,
                                  					isnull(it.color, '') AS color,
                                  					isnull(it.size, '') AS size,
                                  					isnull(it.style, '') AS style,
                                  					o.[user_id],
                                  					ol.unit_qty_chg,
                                  					o.est_delivery_date,
                                  					vendloc.location_type,
                                  					CASE
                                  						WHEN vendloc.location_type = 'warehouse' OR vendloc.location_type = 'store'
                                  							THEN inloctransfer.INVENTSITEID --transfer order
                                  						WHEN vendloc.location_type = 'vendor'
                                  							THEN inloc.InventSiteId
                                  					END AS SITEID,
                                  					'' AS CHANNELID,
                                  					CASE
                                  						WHEN vendloc.location_type = 'warehouse' OR vendloc.location_type = 'store'
                                  							THEN inloctransfer.INVENTLOCATIONID --transfer order
                                  						WHEN vendloc.location_type = 'vendor'
                                  							THEN ''
                                  					END AS WAREHOUSE
                                  					FROM dbo.orders o
                                  					INNER JOIN dbo.order_lines ol ON o.id = ol.order_id
                                  					INNER JOIN dbo.locations vendloc ON o.order_from_location_id = vendloc.id
                                  					INNER JOIN dbo.locations loc ON loc.id = o.location_id
                                  					INNER JOIN dbo.items it ON it.id = ol.item_id
                                  					INNER JOIN agr5_stg.ax.INVENTLOCATION inloc ON inloc.INVENTLOCATIONID = loc.location_no
                                  --					INNER JOIN agr5_stg.ctr.DataAreas da ON da.DataAreaId = inloc.DATAAREAID
                                  					LEFT JOIN  agr5_stg.ax.INVENTLOCATION inloctransfer ON inloctransfer.INVENTLOCATIONID = vendloc.location_no  --AND inloctransfer.DATAAREAID = da.DATAAREAID
                                  					WHERE o.[status] = 1 AND ol.unit_qty_chg > 0 AND o.deleted = 0
                                  GO
                                    */
GO