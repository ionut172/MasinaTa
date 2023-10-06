'use client'
import { useParamsStore } from "@/hooks/useParamsStore";
import { useParams } from "next/navigation";

import React from "react";
import Header from "./Header";
import { Button } from "flowbite-react";
import { signIn } from "next-auth/react";
type Props = {
    title?: string
    subtitle?: string
    showReset? : boolean
    showLogin?: boolean
    callbackUrl?: string
}

export default function Empty({title = "Nu s-au gasit masini pentru filtru.", subtitle ="Schimba filtrul.", showReset, showLogin, callbackUrl} : Props) {
    const reset = useParamsStore (state =>state.reset);
    return (
        <div className="h-[40vh] flex flex-col gap-2 justify-center items-center shadow-lg">
            <Header title={title} subtitle={subtitle} center/>
            <div className="mt-4">
                {showReset && (
                    <Button outline onClick={reset}>Sterge filtrele</Button>
                )}
                {
                    showLogin && (
                        <Button outline onClick={()=>signIn('id-server', {callbackUrl})} color="black">Login</Button>      
                    )
                }
            </div>

        </div>
    )
}