app.factory('jobService', [
    '$http', '$q', 'db.util', function($http, $q, util) {
        var v = 'api/v1/';

        var calculatePaging = util.calculatePaging;
        var orderBy = util.orderBy;

        var job = {};

        job.get = function(id) {
            return $http.get(v + 'company/job/' + id);
        }

        job.paged = function(company,page, pageSize) {
            return $http.get(v + 'job/company/'+company+'?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
        }

        job.create = function(formData) {
            return $http.post(v + 'company/job', formData);
        }

        job.update = function(record) {
            return $http.put(v + 'company/job', record);
        }

        job.publish = function(id) { return $http.put(v + 'company/job/publish', { id: id }); }
        job.unpublish = function(id) { return $http.put(v + 'company/job/unpublish', { id: id }); }
        job.cancel = function(id) { return $http.put(v + 'company/job/cancel', { id: id }); }
        job.delete = function(id) { return $http.delete(v + 'company/job/' + id); }

        return job;
    }
]);
