-- =============================================
-- Author:    Thordur Oskarsson
-- Create date: 22.08.2017.TO
-- Description: 
--
-- 22.11.2017.TO Added product info and the new variant structure
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
,vdsn.[no] AS size_no
,vdsn.name AS size
,vdcn.[no] AS color_no
,vdcn.name AS color
,vdcg.[no] AS color_group_no
,vdcg.name AS color_group
,vdsg.[no] AS size_group_no
,vdsg.name AS size_group
,pc.[status] AS master_status
,s.[no] AS primary_vendor_no
,pri.sale_price
,pri.cost_price
,osci.min_order_qty
,osci.pack_size
,osci.display_stock
,o.id AS option_id
FROM [bm].[products_created] pc
INNER JOIN [bm].[product_info] pri ON pc.product_id = pri.product_id
--INNER JOIN [bm].[product_variant_columns] pvc ON pvc.id = pc.variant_id
--INNER JOIN [bm].[product_variant_details] pvd ON pvc.variant_details_id_1 = pvd.id
INNER JOIN [bm].[options_created] oc ON oc.product_id = pc.product_id
INNER JOIN [bm].[option_group_columns] ogc ON ogc.id = oc.option_group_id
INNER JOIN [bm].[option_group_details] div ON div.id = ogc.group_details_id_1
INNER JOIN [bm].[option_group_details] dep ON dep.id = ogc.group_details_id_2
INNER JOIN [bm].[option_group_details] sub_dep ON sub_dep.id = ogc.group_details_id_3
INNER JOIN [bm].[option_variant_columns] ovc ON ovc.id = oc.variant_id
INNER JOIN [bm].[option_variant_details] op_name ON op_name.id = ovc.[option]
INNER JOIN [bm].[option_items_created] oic ON oic.option_id = oc.option_id
INNER JOIN [bm].[variant_columns_colour] vcc ON vcc.id = oic.colour_id
INNER JOIN [bm].[variant_columns_size] vcs ON vcs.id = oic.size_id
INNER JOIN [bm].[variant_columns_style] vcst ON vcst.id = oic.style_id
INNER JOIN [bm].[variant_detail_colour_names] vdcn ON vdcn.id = vcc.colour_name_id
INNER JOIN [bm].[variant_detail_colour_groups] vdcg ON vdcg.id = vcc.colour_group_id
INNER JOIN [bm].[variant_detail_size_names] vdsn ON vdsn.id = vcs.size_name_id
INNER JOIN [bm].[variant_detail_size_groups] vdsg ON vdsg.id = vcs.size_group_id
INNER JOIN [bm].[variant_detail_style_names] vdstn ON vdstn.id = vcst.style_name_id
INNER JOIN [bm].[variant_detail_style_groups] vdstg ON vdstg.id = vcst.style_group_id
INNER JOIN [bm].[option_season_code_info] osci ON osci.[option_id] =  oc.option_id
--INNER JOIN [bm].[variant_group_ratios] vgr ON osci.variant_group_ratio_id = vgr.id
--INNER JOIN [bm].[variant_groups] vg ON vg.id = vgr.variant_group_id
INNER JOIN [bm].[option_items] oi ON oi.id = oic.option_item_id
INNER JOIN [bm].[options] o ON o.id = oi.option_id
INNER JOIN [bm].[suppliers] s ON s.id = pri.primary_supplier_id
where o.option_status_id = 1





GO


