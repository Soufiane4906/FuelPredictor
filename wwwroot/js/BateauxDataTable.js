$(document).ready(function () {

    $('#Bateaux').dataTable({

        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Bateaux/GetBateaux",
            "type": "POST",
            "datatype" : "json",

        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable" :false
        }],

       /*                 < th > NOM_BATEAU</th >
                <th>CODE_BATEAU</th>
                <th>GENRE</th>
                <th>TYPE</th>
                <th>TYPE_PECHE</th>
                <th>Segment_Peche</th>
                <th>PORT</th>
                <th>Action</th>*/

        "columns": [
           {"data" : "id" , name:"Id" , "autowidth" : true},
            { "data": "noM_BATEAU", name:"NOM_BATEAU" , "autowidth" : true},
            { "data": "codE_BATEAU", name:"CODE_BATEAU" , "autowidth" : true},
            { "data": "genre", name:"GENRE" , "autowidth" : true},
            { "data": "type", name:"TYPE_PECHE" , "autowidth" : true},
            { "data": "typE_PECHE", name:"TYPE_PECHE" , "autowidth" : true},
            { "data": "segment_Peche", name:"Segment_Peche" , "autowidth" : true},
            { "data": "port", name:"PORT" , "autowidth" : true},
            { "data": "etat", name:"ETAT" , "autowidth" : true},
            { "data": "date_MAJ", name: "Date_MAJ", "autowidth": true },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                <a href="Bateaux/Edit/${row.id}">Edit</a> |
                <a href="Bateaux/Vue/${row.id}">Vue</a> |
                <a href="Bateaux/Details/${row.id}">Details</a> |
                <a href="Bateaux/Delete/${row.id}">Delete</a>
            `;
                }
            }
        ]

    });

});