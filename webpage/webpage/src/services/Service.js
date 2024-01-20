import axios from "axios";

export class Service{
    emitter;
    pathName = ''
    _http = axios.create({
        baseURL:"http://localhost:5170/integration/",
    });
    constructor(emitter) {
        this._http.interceptors.request.use((config) => {
            if(emitter){
                emitter.emit('loading', true)
            }
            return config;
        }, (error) => {
            if(emitter){emitter.emit('loading', false)}
            console.error('Error during request:', error);
            return Promise.reject(error);
        });
        this._http.interceptors.response.use((response) => {
            if(emitter){emitter.emit('loading', false)}

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

}