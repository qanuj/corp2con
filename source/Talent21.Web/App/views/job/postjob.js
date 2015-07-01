app.controller('postjobsController', ['$scope', 'dataService', function ($scope, db) {

    $scope.save = function (record) {
        record.skills = [];
        db.job.newjob(record).success(function (result) {
            console.log(result);
        });
    }
}]);