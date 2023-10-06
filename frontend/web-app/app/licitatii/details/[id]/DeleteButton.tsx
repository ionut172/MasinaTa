'use client'
import { deleteLicitatii } from "@/app/actions/licitatiiActions";
import { fetchWrapper } from "@/lib/fetchWrapper";
import { error } from "console";
import { Button } from "flowbite-react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import React, { useState } from "react";
import toast from "react-hot-toast";

type Props = {
    id: string
}

export default function DeleteButton ({id}: Props){
    const [loading, setLoading] = useState(false);
    const router = useRouter();
    function doDelete(){
        setLoading(true);
        deleteLicitatii(id).then(res=> {
            if(res.error) throw res.error();
            router.push("/")
             }).catch(error => {
                toast.error(error.status + ' ' + error.message);
             }).finally(()=>setLoading(false));
    }

    return (
        <div>
       <Button color="failure" outline isProcessing={loading} onClick={doDelete}>
            <Link href={`/licitatii/update/${id}`}>Sterge licitatia</Link>
       </Button>
  
       
  </div>
    )
}