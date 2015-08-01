app.controller('contractorJobController', ['$scope', 'dataService', '$routeParams', '$window', function ($scope, db, param, $window) {
    $scope.isCollapsed = true;
    $scope.title = "Job Profile";
    $scope.role = db.role;
    $scope.id = param.id;


    function getApplications() {
        db.contractor.getJobApplicationsByJobId(param.id).success(function (result) {
            var job = result.items[0];
            if (!job) return;
            
            console.log(result);

            for (var act in job.actions) {
                $scope.record.applicationId = job.actions[act].applicationId;//job application id;
                if (job.actions[act].act == 'Application') {
                    $scope.record.isApplied = true;
                    $scope.record.applied = job.actions[act].created;
                }
                if (job.actions[act].act == 'Revoke') {
                    $scope.record.isApplied = false;
                    $scope.record.revoked = job.actions[act].created;
                }
                if (job.actions[act].act == 'Favorite') {
                    $scope.record.isFavorite = true;
                    $scope.record.favorite = job.actions[act].created;
                }
            }
        });
    }

    db.contractor.jobById($scope.id).success(function (result) {
        $scope.record = result;
        getApplications();
    });

    $scope.revoke = function (record) {
        db.job.revoke(record.applicationId).success(function (result) {
            console.log('Job Cancelled');
        });
    }

    $scope.apply = function (record) {
        db.contractor.ApplyToJob(record.applicationId).success(function (result) {
            $scope.record.isApplied = true;
            $scope.record.applied = new Date();
        });
    }

    $scope.favorite = function (record) {
        db.contractor.favorite(record.applicationId).success(function (result) {
            $scope.record.isFavorite = true;
            $scope.record.favorite = new Date();
            //$window.history.back();
        });
    }

    $scope.unfavorite = function (record) {
        db.contractor.unfavorite(record.applicationId).success(function (result) {
            $scope.record.isFavorite = false;
            //$window.history.back();
        });
    }
}]);