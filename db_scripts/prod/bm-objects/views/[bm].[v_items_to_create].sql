-- =============================================
-- Author:    Thordur Oskarsson
-- Create date: 22.08.2017.TO
-- Description:
-- =============================================
CREATE VIEW [bm].[v_items_to_create]
AS

SELECT DISTINCT
pc.temp_id
,pc.[product_no]
,pc.[name] AS product_name
,pc.[description]
-- ,pvd.variant_no
--,pvd.name
,div.group_no AS divison_no
,div.name AS divison
,dep.group_no AS department_no
,dep.name AS department
,sub_dep.group_no AS sub_department_no
,sub_dep.name AS sub_department
,op_name.variant_no AS option_name_no
,op_name.name AS option_name
,size.variant_no AS size_no
,size.name AS size
,color.variant_no AS color_no
,color.name AS color
,color_group.variant_no AS color_group_no
,color_group.name AS color_group
,[variant_group_no] AS size_group_no
,[variant_group_name] AS size_group
, pc.[status] as master_status
, osci.min_order_qty
, osci.pack_size
, osci.display_stock
, o.id as option_id
FROM [bm].[products_created] pc
INNER JOIN [bm].[product_variant_columns] pvc ON pvc.id = pc.variant_id
INNER JOIN [bm].[product_variant_details] pvd ON pvc.variant_details_id_1 = pvd.id
INNER JOIN [bm].[options_created] oc ON oc.product_id = pc.temp_id
INNER JOIN [bm].[option_group_columns] ogc ON ogc.id = oc.option_group_id
INNER JOIN [bm].[option_group_details] div ON div.id = ogc.group_details_id_1
INNER JOIN [bm].[option_group_details] dep ON dep.id = ogc.group_details_id_2
INNER JOIN [bm].[option_group_details] sub_dep ON sub_dep.id = ogc.group_details_id_3
INNER JOIN [bm].[option_variant_columns] ovc ON ovc.id = oc.variant_id
INNER JOIN [bm].[option_variant_details] op_name ON op_name.id = ovc.[option]
INNER JOIN [bm].[option_items_created] oic ON oic.option_id = oc.temp_id
INNER JOIN [bm].[option_item_variant_columns] oivc ON oivc.id = oic.variant_id
INNER JOIN [bm].[option_item_variant_details] size ON oivc.size = size.id
INNER JOIN [bm].[option_item_variant_details] color ON oivc.color = color.id
INNER JOIN [bm].[option_item_variant_details] color_group ON oivc.variant_details_id_3 = color_group.id
INNER JOIN [bm].[option_season_code_info] osci ON osci.[option_id] =  oc.temp_id
INNER JOIN [bm].[variant_group_ratios] vgr ON osci.variant_group_ratio_id = vgr.id
INNER JOIN [bm].[variant_groups] vg ON vg.id = vgr.variant_group_id
INNER JOIN [bm].[option_items] oi on oi.id = oic.temp_id
INNER JOIN [bm].[options] o on o.id = oi.option_id
where o.option_status_id = 1


GO