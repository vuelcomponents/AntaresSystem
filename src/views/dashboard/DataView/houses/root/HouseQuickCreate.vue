

<template>
<div class="multiple-create">
  <div :hidden="hide(param)" class="box" v-for="param in house.getInputs('text',null,true)" :key="param">
    <p class="form-label">
      {{house.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="house[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in house.getInputs('numeric',null,true)" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{house.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="house.getHeaders([param],'first')" class="form-text-input" v-model="house[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in house.getInputs('dates',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{house.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="house[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in house.getInputs('collections',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{house.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="house[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   house.create.includes(param) ? 'create' : '',
                                   house.select.includes(param) ? 'select' : '',
                                   house.delete.includes(param) ? 'delete' : '',
                                   house.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="house"
                         owner-key="house"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="house[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in house.getInputs('objects',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{house.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="house" :param="param" :disabled="lock(param)" :model="house"></SelectionManager>
    </div>
  </div>
  <div v-for="param in house.getInputs('files',null,true)" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{house.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="house.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="house.setFile($event, param)" />
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
    <div  :hidden="hide(param)" class="box" v-for="param in house.getInputs('textArea',null,true)" :key="param">
      <p class="form-label" >
        {{house.getHeaders([param],"first")}}
      </p>
      <Editor  :disabled="lock(param)" v-model="house[param]" class="form-text-input"/>
    </div>
  </div>




</template>
<script>

import { inject } from "vue";
import {HouseService} from "@/services/HouseService";
import Toast from "@/utils/classes/Toast";
import HouseCrudController from "@/views/dashboard/DataView/houses/controllers/HouseCrudController";
import HouseClass from "@/views/dashboard/DataView/houses/HouseClass";
import CreateOptions from "@/views/dashboard/DataView/houses/options/CreateOptions";
import HouseLife from "@/views/dashboard/DataView/houses/HouseLife";



export default {
  components: {},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new HouseService(emitter)
    };
    const toast = new Toast()
    const crud = new HouseCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      house: new HouseClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new HouseLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.house).then(res=>{
        if(res?.invalid){
          console.log('invalid house create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          if(res.data) {
            this.emitter.emit('quickHouseAdd', res.data)
          }
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.house[key] = collection
      }
    },
    lock(param){
      if(!this.house.$lock[param]){
        return false;
      }
      return this.house.$lock[param]()
    },
    hide(param){
      if(!this.house.$hide[param]){
        return false;
      }
      return this.house.$hide[param]()
    }
  }
}
</script>
<style >

</style>