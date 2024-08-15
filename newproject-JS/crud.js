let  data=[
       
    {id:1 , name:"aaa", email:"aaa@gmail.com"},
    {id:2, name:"bbb", email:"bbb@gmail.com"}
    
]
let counter = data.length + 1;
function readall(){
    localStorage.setItem("object",JSON.stringify(data));
    var tabledata=document.querySelector(".data-table");
    var object=localStorage.getItem('object');
    var objectdata=JSON.parse(object);
    var elements="";
    objectdata.map(record=>(
        elements +=`<tr>
        <td>${record.name} </td>
        <td>${record.email} </td>
        <td>
        <button class="edit" onclick={edit(${ record.id})}>Edit</botton>
        <button class="delete" onclick="delet(${ record.id})">Delete</botton>
       
        
        </td>
        </tr>`
    ))
    tabledata.innerHTML=elements;
   
    }
function delet(id){
        data=data.filter(rec=>rec.id!==id);
        
        // Update the data in local storage
        localStorage.setItem("object", JSON.stringify(data));
        // Re-render the table
        readall();
    }

function create(){
    document.querySelector(".create-form").style.display="block";
    document.querySelector(".add-div").style.display="none";
}
function add(){
    var name=document.querySelector(".name").value;
    var email=document.querySelector(".email").value;
    var newObj={id:counter++, name:name,email:email};
    data.push(newObj);
    document.querySelector(".create-form").style.display="none";
    document.querySelector(".add-div").style.display="block";
    readall();
}
function edit(id){
    document.querySelector('.update-form').style.display="block";
    var obj=data.find(rec=>rec.id===id);

    document.querySelector('.uname').value=obj.name;
    document.querySelector('.uemail').value=obj.email;
    document.querySelector('.id').value=obj.id;
}
function update(){
    var id=parseInt(document.querySelector('.id').value);
    var name=document.querySelector('.uname').value;
    var email=document.querySelector('.uemail').value;
    var index=data.findIndex(rec=>rec.id===id);
    data[index]={id,name,email};
    document.querySelector('.update-form').style.display="none";
    readall();

}