app.controller('contractorApplicationController', ['$scope', 'dataService', function ($scope, db) {
    $scope.title = "Applications : ";
    $scope.save = function (record) {
        record.skills = [];
        db.job.ApplyToJob(record).success(function (result) {
            console.log(result);
        });
    }
}]);