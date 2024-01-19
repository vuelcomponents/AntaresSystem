

<template>
<div class="multiple-create">
  <div :hidden="hide(param)" class="box" v-for="param in xdocument.getInputs('text',null,true)" :key="param">
    <p class="form-label">
      {{xdocument.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="xdocument[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in xdocument.getInputs('numeric',null,true)" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="xdocument.getHeaders([param],'first')" class="form-text-input" v-model="xdocument[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in xdocument.getInputs('dates',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="xdocument[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in xdocument.getInputs('collections',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="xdocument[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   xdocument.create.includes(param) ? 'create' : '',
                                   xdocument.select.includes(param) ? 'select' : '',
                                   xdocument.delete.includes(param) ? 'delete' : '',
                                   xdocument.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="xdocument"
                         owner-key="xdocument"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="xdocument[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in xdocument.getInputs('objects',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="xdocument" :param="param" :disabled="lock(param)" :model="xdocument"></SelectionManager>
    </div>
  </div>
  <div v-for="param in xdocument.getInputs('files',null,true)" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="xdocument.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="xdocument.setFile($event, param)" />
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


import DocumentCrudController from "@/views/dashboard/DataView/documents/controllers/DocumentCrudController";
import DocumentClass from "@/views/dashboard/DataView/DocumentClass";
import DocumentLife from "@/views/dashboard/DataView/documents/DocumentLife";
import CreateOptions from "@/views/dashboard/DataView/documents/options/CreateOptions";
import Toast from "@/utils/classes/Toast";
import {DocumentService} from "@/services/DocumentService";
import { inject } from "vue";
export default {
  components: {},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new DocumentService(emitter)
    };
    const toast = new Toast()
    const crud = new DocumentCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      xdocument: new DocumentClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new DocumentLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.xdocument).then(res=>{
        if(res?.invalid){
          console.log('invalid xdocument create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          if(res.data) this.emitter.emit('quickDocumentAdd', res.data)
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.xdocument[key] = collection
      }
    },
    lock(param){
      if(!this.xdocument.$lock[param]){
        return false;
      }
      return this.xdocument.$lock[param]()
    },
    hide(param){
      if(!this.xdocument.$hide[param]){
        return false;
      }
      return this.xdocument.$hide[param]()
    }
  }
}
</script>
<style >

</style>