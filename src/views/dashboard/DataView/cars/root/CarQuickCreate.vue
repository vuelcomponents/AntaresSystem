

<template>
<div class="multiple-create">
  <div :hidden="hide(param)" class="box" v-for="param in car.getInputs('text',null,true)" :key="param">
    <p class="form-label">
      {{car.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="car[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in car.getInputs('numeric',null,true)" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{car.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="car.getHeaders([param],'first')" class="form-text-input" v-model="car[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in car.getInputs('dates',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{car.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="car[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in car.getInputs('collections',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{car.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="car[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   car.create.includes(param) ? 'create' : '',
                                   car.select.includes(param) ? 'select' : '',
                                   car.delete.includes(param) ? 'delete' : '',
                                   car.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="car"
                         owner-key="car"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="car[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in car.getInputs('objects',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{car.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="car" :param="param" :disabled="lock(param)" :model="car"></SelectionManager>
    </div>
  </div>
  <div v-for="param in car.getInputs('files',null,true)" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{car.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="car.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="car.setFile($event, param)" />
    </div>
  </div>
  <div  style="display:flex; align-items:flex-end;gap:2px; padding:2px; padding-inline:6px;">
    <button class="button-quick" @click="create" style="margin:0;">
      <i class="mdi mdi-link-plus" />
      Create
    </button>
  </div>
</div>
  <div class="multiple-create" style="display:block" :key="life.counterKey">
    <div  :hidden="hide(param)" class="box" v-for="param in car.getInputs('textArea',null,true)" :key="param">
      <p class="form-label" >
        {{car.getHeaders([param],"first")}}
      </p>
      <Editor  :disabled="lock(param)" v-model="car[param]" class="form-text-input"/>
    </div>
  </div>




</template>
<script>

import { inject } from "vue";
import {CarService} from "@/services/CarService";
import Toast from "@/utils/classes/Toast";
import CarCrudController from "@/views/dashboard/DataView/cars/controllers/CarCrudController";
import CarClass from "@/views/dashboard/DataView/cars/CarClass";
import CreateOptions from "@/views/dashboard/DataView/cars/options/CreateOptions";
import CarLife from "@/views/dashboard/DataView/cars/CarLife";



export default {
  components: {},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new CarService(emitter)
    };
    const toast = new Toast()
    const crud = new CarCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      car: new CarClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new CarLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.car).then(res=>{
        if(res?.invalid){
          console.log('invalid car create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          if(res.data) {
            this.emitter.emit('quickCarAdd', res.data)
          }
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.car[key] = collection
      }
    },
    lock(param){
      if(!this.car.$lock[param]){
        return false;
      }
      return this.car.$lock[param]()
    },
    hide(param){
      if(!this.car.$hide[param]){
        return false;
      }
      return this.car.$hide[param]()
    }
  }
}
</script>
<style >

</style>