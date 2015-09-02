app.controller('contractorJobController', ['$scope', 'dataService', '$routeParams', '$window', function ($scope, db, param, $window) {
    $scope.isCollapsed = true;
    $scope.title = "Job Profile";
    $scope.role = db.role;
    $scope.id = param.id;

    $scope.reasons = ["Not Available", "No Reason"];

    db.contractor.jobById($scope.id).success(function (job) {
        $scope.record = job;
        for (var act in job.actions) {
            $scope.record.applicationId = job.actions[act].applicationId;//job application id;
            $scope.record[job.actions[act].act.toLowerCase()] = job.actions[act].created;
        }
        db.contractor.visitJob(param.id);
    });

    $scope.revoke = function (record) {
        db.contractor.revoke(record.id).success(function (result) {
            $scope.record.revoke = new Date();
        });
    }

    $scope.apply = function (record) {
        db.contractor.applyToJob(record.id).success(function (result) {
            $scope.record.application = new Date();
        });
    }

    $scope.decline = function (record, reason) {
        if (!reason) return;//todo:show message;
        db.contractor.declineToJob(record.id, reason).success(function (result) {
            $scope.record.decline = new Date();
        });
    }

    $scope.favorite = function (record) {
        db.contractor.favorite(record.id).success(function (result) {
            $scope.record.favorite = new Date();
        });
    }

    $scope.unfavorite = function (record) {
        db.contractor.unfavorite(record.id).success(function (result) {
            delete $scope.record.favorite;
        });
    }
}]);