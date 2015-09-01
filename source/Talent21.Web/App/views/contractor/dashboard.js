app.controller('contractorDashboardController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {

    db.contractor.dashboard().success(function (result) {
        $scope.record = result;
        db.contractor.search({ location: result.aggregate.location, skills: result.aggregate.skill }, 1, 5, 'Days').then(function (result) {
            $scope.matching = result.items;
        });
    });
    
    $scope.search = function (q) {
        window.location = '#/search?q=' + (q.keywords || '') + '&location=' + (q.location || '') + '&skills=' + (q.skills || '');
        return false;
    }

    db.contractor.get().success(function (result) {
        $scope.profile = result;
        if (!result.firstName && !result.lastName) {
            window.location = "#/profile/edit";
        }
    });
}]);

