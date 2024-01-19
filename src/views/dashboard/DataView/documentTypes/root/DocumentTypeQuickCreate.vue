

<template>
<div class="multiple-create">
  <div :hidden="hide(param)" class="box" v-for="param in documentType.getInputs('text',null,true)" :key="param">
    <p class="form-label">
      {{documentType.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="documentType[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in documentType.getInputs('numeric',null,true)" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{documentType.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="documentType.getHeaders([param],'first')" class="form-text-input" v-model="documentType[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in documentType.getInputs('dates',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{documentType.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="documentType[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in documentType.getInputs('collections',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{documentType.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="documentType[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   documentType.create.includes(param) ? 'create' : '',
                                   documentType.select.includes(param) ? 'select' : '',
                                   documentType.delete.includes(param) ? 'delete' : '',
                                   documentType.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="documentType"
                         owner-key="documentType"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="documentType[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in documentType.getInputs('objects',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{documentType.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="documentType" :param="param" :disabled="lock(param)" :model="documentType"></SelectionManager>
    </div>
  </div>
  <div v-for="param in documentType.getInputs('files',null,true)" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{documentType.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="documentType.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="documentType.setFile($event, param)" />
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
import { inject } from "vue";
import DocumentTypeClass from "@/views/dashboard/DataView/documentTypes/DocumentTypeClass";
import Toast from "@/utils/classes/Toast";
import DocumentTypeCrudController from "@/cruds/DocumentTypeCrudController";
import CreateOptions from "@/views/dashboard/DataView/documentTypes/options/CreateOptions";
import DocumentTypeLife from "@/views/dashboard/DataView/documentTypes/DocumentTypeLife";
import {DocumentTypeService} from "@/services/DocumentTypeService";
export default {
  components: {},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new DocumentTypeService(emitter)
    };
    const toast = new Toast()
    const crud = new DocumentTypeCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      documentType: new DocumentTypeClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new DocumentTypeLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.documentType).then(res=>{
        if(res?.invalid){
          console.log('invalid documentType create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          if(res.data) this.emitter.emit('quickDocumentTypeAdd', res.data)
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.documentType[key] = collection
      }
    },
    lock(param){
      if(!this.documentType.$lock[param]){
        return false;
      }
      return this.documentType.$lock[param]()
    },
    hide(param){
      if(!this.documentType.$hide[param]){
        return false;
      }
      return this.documentType.$hide[param]()
    }
  }
}
</script>
<style >

</style>