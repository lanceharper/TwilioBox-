var o = {
    init: function () {
        this.map.init();
    },
    map: {
        size: function () {
            var w = $(window).width(),
                h = $(window).height();
            return { width: w, height: h };
        },
        data: {
            zoom: 5,
            center: new google.maps.LatLng(39.1574230, -94.8327590),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        },
        init: function () {
            var size = o.map.size();

            $('#map').css({ width: size.width, height: size.height });
            map = new google.maps.Map(document.getElementById('map'), o.map.data),
            geocoder = new google.maps.Geocoder();

            o.map.loadMarkers();
        },
        loadMarkers: function () {
           
            
            var kcMarker = new google.maps.Marker({
                //icon: img,
                map: map,
                position: new google.maps.LatLng(39.2029110, -94.5883870),
                //icon: 'https://chart.googleapis.com/chart?chst=d_map_spin&chld=1|0|FFFFFF|16|b|' + ev.LocationId
                //icon: mapiconFolder + ev.SwipeStatus + '.png',
                tag: 'Kansas City',
                number: '(816) 555-5555',
                folderId: 105995722
            });

           
            var sfMarker = new google.maps.Marker({
                //icon: img,
                map: map,
                position: new google.maps.LatLng(37.8120, -122.34820),
                //icon: 'https://chart.googleapis.com/chart?chst=d_map_spin&chld=1|0|FFFFFF|16|b|' + ev.LocationId
                //icon: mapiconFolder + ev.SwipeStatus + '.png',
                tag: 'San Francisco',
                number: '(415) 555-5555',
                folderId: 105995756
            });
            
            google.maps.event.addListener(kcMarker, 'click', function () {

                $("#uploadKCDialog").find("h4").html(this.tag);
                $("#uploadKCDialog").find("h5").html(this.number);
                
                $("#uploadKCDialog").dialog("open");
            });

            google.maps.event.addListener(sfMarker, 'click', function () {

                $("#uploadSFDialog").find("h4").html(this.tag);
                $("#uploadSFDialog").find("h5").html(this.number);
               
                $("#uploadSFDialog").dialog("open");
            });
            
        }
    }
};

