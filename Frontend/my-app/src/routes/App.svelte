<script>
	import { onMount } from "svelte"; 
  const endpoint = "http://localhost:5029/alkalmazott/getalkalmazottbyauto"; 
  let posts = [];
  onMount(async function () {
    const response = await fetch(endpoint);
    const data = await response.json();
    console.log(data);
	posts=data;
  });
</script>
{#each posts as alkalmazott}
  <div>    <p>{alkalmazott.alkNev}</p>  </div>
{/each}
