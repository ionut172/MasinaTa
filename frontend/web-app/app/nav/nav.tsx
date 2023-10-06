
import React from "react"
import {AiOutlineCar } from "react-icons/ai";
import Search from "./Search";
import Logo from "./Logo";
import LoginButton from "./LoginButton";
import { getCurrentUser } from "../actions/authActions";
import UserAction from "./UserActions";

export default async function Navbar() {
    const user = await getCurrentUser();
    console.log(user + " Vin de aici");
    return (
        <header className="sticky top-0 z-50 flex justify-between bg-white p-5 items-center text-gray-800 shadow-md">
         <Logo/>
           <Search/>
           
           {user ? (
        <UserAction user={user}/>
       
      ) : (
        <LoginButton />
      )}
      
        </header>
    )
}