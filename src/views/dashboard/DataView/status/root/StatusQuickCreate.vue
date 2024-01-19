

<template>
<div class="multiple-create">
  <div :hidden="hide(param)" class="box" v-for="param in status.getInputs('text',null,true)" :key="param">
    <p class="form-label">
      {{status.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="status[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in status.getInputs('numeric',null,true)" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{status.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="status.getHeaders([param],'first')" class="form-text-input" v-model="status[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in status.getInputs('dates',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{status.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="status[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in status.getInputs('collections',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{status.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="status[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   status.create.includes(param) ? 'create' : '',
                                   status.select.includes(param) ? 'select' : '',
                                   status.delete.includes(param) ? 'delete' : '',
                                   status.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="status"
                         owner-key="status"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="status[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in status.getInputs('objects',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{status.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="status" :param="param" :disabled="lock(param)" :model="status"></SelectionManager>
    </div>
  </div>
  <div v-for="param in status.getInputs('files',null,true)" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{status.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="status.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="status.setFile($event, param)" />
    </div>
  </div>
  <div  style="display:flex; align-items:flex-end;gap:2px; padding:2px; padding-inline:6px;">
    <button class="button-quick" @click="create" style="margin:0;">
      <i class="mdi mdi-link-plus" />
      Create
    </button>
  </div>
</div>




</template>
<script>


import {StatusService} from "@/services/StatusService";
import Toast from "@/utils/classes/Toast";
import StatusCrudController from "@/views/dashboard/DataView/status/controllers/StatusCrudController";
import StatusClass from "@/views/dashboard/DataView/status/StatusClass";
import CreateOptions from "@/views/dashboard/DataView/status/options/CreateOptions";
import StatusLife from "@/views/dashboard/DataView/status/StatusLife";
import { inject } from "vue";
export default {
  components: {},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new StatusService(emitter)
    };
    const toast = new Toast()
    const crud = new StatusCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      status: new StatusClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new StatusLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.status).then(res=>{
        if(res?.invalid){
          console.log('invalid status create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          if(res.data) this.emitter.emit('quickStatusAdd', res.data)
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.status[key] = collection
      }
    },
    lock(param){
      if(!this.status.$lock[param]){
        return false;
      }
      return this.status.$lock[param]()
    },
    hide(param){
      if(!this.status.$hide[param]){
        return false;
      }
      return this.status.$hide[param]()
    }
  }
}
</script>
<style >

</style>