var o = {
    init: function() {
        this.map.init();
    },
    map: {
        size: function() {
            var w = $(window).width(),
                h = $(window).height();
            return { width: w, height: h };
        },
        data: {
            zoom: 5,
            center: new google.maps.LatLng(39.1574230, -94.8327590),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        },
        init: function() {
            var size = o.map.size();

            $('#map').css({ width: size.width, height: size.height });
            map = new google.maps.Map(document.getElementById('map'), o.map.data),
            geocoder = new google.maps.Geocoder();

            o.map.loadMarkers();
        },
        loadMarkers: function() {
            var infowindow = new google.maps.InfoWindow();

            var marker = new google.maps.Marker({
                    //icon: img,
                    map: map,
                    position: new google.maps.LatLng(39.2029110, -94.5883870),
                    //icon: 'https://chart.googleapis.com/chart?chst=d_map_spin&chld=1|0|FFFFFF|16|b|' + ev.LocationId
                    //icon: mapiconFolder + ev.SwipeStatus + '.png',
                });
            infowindow.setContent($("#fileupload").html());
            infowindow.open(map, marker);
        }
    }
};

