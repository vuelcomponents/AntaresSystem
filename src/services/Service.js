import axios from "axios";
import moment from "moment";
import {handleDates} from "@/utils/functions/isIsoDate";
export class Service{

    emitter;

    pathName = ''
    _http = axios.create({
        baseURL:"http://localhost:7057/api/",
    });
    constructor(emitter) {
        this._http.interceptors.request.use((config) => {
            if(emitter){emitter.emit('loading', true)}
            // if(config.data){
            //     handleDates(config.data, "YYYY-MM-DD")
            // }
            return config;
        }, (error) => {
            if(emitter){emitter.emit('loading', false)}
            console.error('Error during request:', error);
            return Promise.reject(error);
        });
        this._http.interceptors.response.use((response) => {
            if(emitter){emitter.emit('loading', false)}
            // if(response.data){
            //     handleDates(response.data, "DD/MM/YYYY")
            // }
            return response;
        }, (error) => {
            if(emitter){
              emitter.emit('loading', false)
              switch(error?.response?.status){
                  case 400:
                      emitter.emit('warn', error?.response?.data ?? error.message)
                  break;
                  default:
                      emitter.emit('error', error?.response?.data ?? error.message)
                  break;
              }

            }
            console.error('Error during response:', error);
            return Promise.resolve(error);
        });
    }
    async getAll(){
        return this._http.get(`${this.pathName}/get`)
    }
    async getById(id){
        return this._http.get(`${this.pathName}/get/${id}`);

    }
    async create(object){
        return this._http.post(`${this.pathName}/create`, object)
    }

    async update(object){
        return this._http.patch(`${this.pathName}/update`, object)
    }
    async delete(id){
        return this._http.delete(`${this.pathName}/delete/${id}`)
    }
    async deleteMultiple(collection){
        return this._http.post(`${this.pathName}/delete-multiple`, collection)
    }
    async deleteMultipleColMan(colManCollection){
        return this._http.post(`${this.pathName}/delete-multiple-colman`, colManCollection)
    }

    async createColMan(colManObject){
        return this._http.post(`${this.pathName}/create-colman`, colManObject)
    }
    async download(id, fileName) {
        try {
            const response = await this._http.get(`${this.pathName}/download/${id}`, { responseType: 'blob' });
            const blob = response.data;
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = `${fileName}`;

            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
            window.URL.revokeObjectURL(url);
        } catch (error) {
            this.emitter.emit('error', error.message)
        }
    }

}