app.controller('companyDashboardController', ['$scope','dataService', function ($scope,db) {
    db.company.dashboard().success(function (result) {
        result.aggregate.duration.min = moment(result.aggregate.duration.min).toDate();
        result.aggregate.duration.max = moment(result.aggregate.duration.max).toDate();
        $scope.record = result;
        
        db.company.search({ location: result.aggregate.location, Skills: result.aggregate.skill }, 1, 5, 'Days').success(function (result) {
            $scope.matching = result.items;
        });
    });

    db.company.get().success(function (result) {
        $scope.profile = result;
    });

    $scope.search = function (q) {
        window.location = '#/search?q=' + (q.keywords || '') + '&location=' + (q.location || '') + '&skills=' + (q.skills || '');
        return false;
    }

}]);