app.factory('adminService', ['$http', '$q', 'db.util', function ($http, $q, util) {
    var v = 'api/v1/admin/';

    var calculatePaging = util.calculatePaging;
    var orderBy = util.orderBy;

    var admin = {};

    admin.get = function() {
        return $http.get(v + 'profile');
    }
    
    admin.transactions=function(page,pageSize) {
        return $http.get(v + 'transaction?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
    }

    admin.transaction = function (id) {
        return $http.get(v + 'transaction/' + id);
    }

    admin.gift = function (email, credits) {
        return $http.post(v + 'gift', { email: email, credit: credits });
    }


    return admin;
}]);