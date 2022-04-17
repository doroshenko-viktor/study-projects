import Head from 'next/head'
import Script from 'next/script'
import Link from 'next/link'
import Layout from '../components/layout'
import { getSortedPostsData } from '../lib/posts';

export default function Home({ posts }) {
  return (
    <Layout home>
      <Head>
        <title>Next.js Example application</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <Script
        src="https://connect.facebook.net/en_US/sdk.js"
        strategy="lazyOnload"
        onLoad={() =>
          console.log(`script loaded correctly, window.FB has been populated`)
        }
      />

      <main>
        <h1 className="title">
          Welcome to <a href="https://nextjs.org">Next.js!</a>
        </h1>

        <Link href="/posts/first-post">first post</Link>
      </main>

      <ul>
        {posts.map(post => {
          return <li>{post.title} - {post.date}</li>
        })}
      </ul>
    </Layout>
  )
}

export async function getStaticProps() {
  const posts = getSortedPostsData();
  return {
    props: {
      posts
    }
  };
}