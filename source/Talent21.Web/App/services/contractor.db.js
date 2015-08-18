app.factory('contractorService', ['$http', '$q','db.util', function ($http, $q,util) {
    var v = 'api/v1/';

    var calculatePaging=util.calculatePaging;
    var orderBy = util.orderBy;

    var contractor={};

    contractor.paged = function (page, pageSize) {
        return $http.get(v + 'contractor/paged?$inlinecount=allpages' + calculatePaging(page,pageSize));
    }

    contractor.dashboard = function () {
        return $http.get(v + 'contractor/dashboard');
    }

    contractor.all = function () {
        return $http.get(v + 'contractor/all');
    }

    contractor.get = function (id) {
        return $http.get(v + 'contractor/profile' + (!!id ? '/' + id : ''));
    }

    contractor.update = function (formData) {
        return $http.put(v + 'contractor/profile', formData);
    }

    contractor.search = function (query, page, pageSize) {
        return $http.post(v + 'job/search?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id'), query);
    }
    
    contractor.jobById = function (id) {
        return $http.get(v + 'contractor/job/' + id);
    }

    contractor.applyToJob = function (id) {
        return $http.post(v + 'contractor/job/' + id + '/apply');
    }

    contractor.revoke = function (id) {
        return $http.post(v + 'contractor/job/' + id + '/revoke');
    }

    contractor.getJobApplications = function (jobId,page,pageSize) {
        return $http.get(v + 'contractor/job/application?$inlinecount=allpages' + calculatePaging(page, pageSize));
    }

    contractor.favorite = function (id) {
        return $http.put(v + 'contractor/job/' + id + '/favorite');
    }

    contractor.unfavorite = function (id) {
        return $http.delete(v + 'contractor/job/' + id + '/favorite');
    }

    contractor.report = function (id) {
        return $http.put(v + 'contractor/job/' + id + '/report');
    }

    contractor.getFavorite = function (page,pageSize) {
        return $http.get(v + 'contractor/job/favorite/paged?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
    }

     contractor.pagedSchedule = function (page, pageSize) {
         return $http.get(v + 'contractor/schedule/paged?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
   }

    contractor.createSchedule = function (formData) {
        return $http.post(v + 'contractor/schedule', formData);
    }

    contractor.editSchedule = function (formData) {
        return $http.put(v + 'contractor/schedule', formData);
    }
    contractor.deleteSchedule = function (record) {
        return $http.delete(v + 'contractor/schedule/' + record.id);
    }

    contractor.getSchedule = function (page,pageSize) {
        return $http.get(v + 'contractor/schedule/all?$orderby=Id desc' + calculatePaging(page,pageSize));
    }

    contractor.getLatestJobs = function (skill, location, page, pageSize) {
        return $http.get(v + 'contractor/latest/jobs/' + skill + '/' + location + '?$orderby=Id desc' + calculatePaging(page, pageSize));
    }

    contractor.topEmployers = function (skill, location, page, pageSize) {
        return $http.get(v + 'contractor/top/employers/' + skill + '/' + location + '?' + calculatePaging(page, pageSize));
    }

    return contractor;
}]);