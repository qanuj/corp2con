app.controller('contractorDashboardController', ['$scope', 'dataService', function ($scope, db) {

    db.contractor.dashboard().success(function (result) {
        $scope.record = result;
    });

    db.contractor.get().success(function (result) {
        $scope.profile = result;
    });

    db.contractor.topEmployers('aspnet', 'mumbai', 1,5).success(function (result) {
        $scope.employers = result;
    });

    db.contractor.getLatestJobs('ruby', 'mumbai', 1, 5).success(function (result) {
        $scope.jobs = result;
    });

}]);