export class Org {
        expandAll = true
        showChart =false

    tree = [];
    dragged;
    hint;
    constructor(){
    }
    setTree(tree){
        this.tree = tree;
        this.distinct()
        this.countBusinessTypeEmployees()
    }
    distinct(tree = this.tree){
        const uniqueIds = new Set();
        function isUnique(node) {
            if (!node || typeof node !== 'object') {
                return true;
            }
            if ('id' in node) {
                if (uniqueIds.has(node.id)) {
                    return false;
                } else {
                    uniqueIds.add(node.id);
                }
            }
            if (Array.isArray(node.children)) {
                node.children = node.children.filter(isUnique);
            }
            return true;
        }

        this.tree = tree.filter(isUnique);
    }
    countBusinessTypeEmployees(tree = this.tree) {
        const sumEmployeesLength = (children) => {
            return children.reduce((sum, child) => {
                if (child.positionUnit?.code === 'Position') {
                    sum += (child.employees ? child.employees.length : 0);
                }
                if (Array.isArray(child.children)) {
                    return sum + sumEmployeesLength(child.children);
                }
                return sum;
            }, 0);
        };
    const processNode = (node) => {
        if (!node || typeof node !== 'object') {
            return;
        }

        if (node.positionUnit?.code === 'Business Unit') {
            node.employeesCount = sumEmployeesLength(node.children);
        }

        if (Array.isArray(node.children)) {
            node.children.forEach(processNode);
        }
    };

    tree.forEach(processNode);
    console.log('umca!', tree)
    }

    draw(coordinates, hint){
        this.hint = document.createElement('div');
        this.hint.style.cssText=`min-width:150px;z-index:9999999999;
        padding:3px;
        background:#fff;position:fixed;top:${coordinates.top}px;left:${coordinates.left}px;
        font-size:0.8em;color:#00000090;border:solid 1px #00000040;display:flex;align-items:center;justify-content:center;
        border-radius:2px;
        `
        this.hint.innerText = hint
        document.body.appendChild(this.hint)
    }
    rubber(){
        if(this.hint && document.body.contains(this.hint)) {
            document.body.removeChild(this.hint)
        }
        this.hint = null;
    }
}